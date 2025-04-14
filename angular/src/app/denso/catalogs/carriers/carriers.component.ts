import { Component, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
    CatalogServiceProxy,
    DocumentTypeDto,
    CarrierDto,
    CarrierServiceDto,
    ServiceDto,
    CurrentCarrierServicesDto,
    CarrierNonWorkingDayDto,
} from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent, DxSchedulerComponent } from 'devextreme-angular';

import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import * as moment from 'moment';
import { SchedulerAppointment } from '../../shared/models/scheduler-appointment';

// https://js.devexpress.com/Documentation/Guide/UI_Components/Scheduler/Timetable/
// https://js.devexpress.com/Demos/WidgetsGallery/Demo/Scheduler/BasicViews/Angular/Light/
// https://github.com/DevExpress-Examples/devextreme-scheduler-create-custom-editing-form

@Component({
    selector: 'carriers',
    templateUrl: './carriers.component.html',
    styleUrls: ['./carriers.component.css'],
    animations: [appModuleAnimation()],
    preserveWhitespaces: true,
})
export class CarriersComponent extends AppComponentBase implements OnInit {
    @Input() carriers: CarrierDto[] = [];
    @Output() onCarrierChanged = new EventEmitter<CarrierDto[]>();

    @Input() services: ServiceDto[] = [];
    @Output() onServiceChanged = new EventEmitter<ServiceDto[]>();

    carriersChanges: CarrierDto = new CarrierDto();

    documentTypes: DocumentTypeDto[] = [];
    currentCarrierServices: CurrentCarrierServicesDto[] = [];

    isTableLoading: boolean = false;
    isLoadingData: boolean = false;
    Editing: boolean = false;
    currentDate: Date = new Date();

    servicesDataSource: DataSource;

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
    @ViewChild(DxSchedulerComponent, { static: false }) scheduler: DxSchedulerComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);

        this.saveDateSelected = this.saveDateSelected.bind(this);
    }

    public ngOnInit(): void {
        this.refresh();

        this.isLoadingData = true;
        Promise.all([
            this._catalogService.getDocumentTypes(undefined, undefined).toPromise(),
            this._catalogService.getServices(undefined, undefined).toPromise(),
            this._catalogService.getCarrierServicesforEdit().toPromise(),
        ]).then((responses) => {
            this.documentTypes = responses[0];
            this.services = responses[1];
            this.currentCarrierServices = responses[2];
            this.isLoadingData = false;
        });
    }

    public onInitNewRow(event: any): void {
        this.carriersChanges = new CarrierDto();
        this.isLoadingData = false;
        this.isTableLoading = false;
        this.carrierServiceMapper();
    }

    public onEditingStart(event: any): void {
        this.carriersChanges = new CarrierDto();
        let newCarrier = new CarrierDto(); //cloneDeep(this.unitMeasureSatChanges);
        newCarrier = this.carriers.find((pn: CarrierDto) => pn.id === event.data.id);
        this.carriersChanges.id = event.data.id;
        this.carriersChanges.name = newCarrier.name;
        this.carriersChanges.divisorNumber = newCarrier.divisorNumber;
        this.carriersChanges.documentTypeId = newCarrier.documentTypeId;
        this.carriersChanges.isActive = newCarrier.isActive;
        this.carriersChanges.nonWorkingDays = newCarrier.nonWorkingDays;
        this.carriersChanges.services = newCarrier.services;

        this.carrierServiceMapper();

        this.Editing = true;

        setTimeout(() => {
            this.loadSchedulerAppointments();
        }, 1000);
    }

    private loadSchedulerAppointments(): void {
        if (this.carriersChanges.nonWorkingDays) {
            this.carriersChanges.nonWorkingDays.forEach((nonWorkingDayItem) => {
                if (nonWorkingDayItem.isActive) {
                    console.log('nonWorkingDayItem', nonWorkingDayItem);
                    let appointment: SchedulerAppointment = new SchedulerAppointment();
                    appointment.id = nonWorkingDayItem.id;
                    appointment.startDate = nonWorkingDayItem.nonWorkingDay.toDate();
                    appointment.endDate = nonWorkingDayItem.nonWorkingDay.toDate();
                    appointment.text = 'Non Work Day';
                    appointment.allDay = true;
                    this.scheduler.instance.addAppointment(appointment);
                }
            });
        }
    }

    public carrierServiceMapper() {
        if (this.carriersChanges.services != null && this.carriersChanges.services.length > 0) {
            for (let item of this.currentCarrierServices) {
                let items = this.carriersChanges.services.find((pn: CarrierServiceDto) => pn.serviceId === item.serviceId);

                if (items != null) {
                    item.isActive = true;
                }
            }
        } else {
            for (let item of this.currentCarrierServices) {
                item.isActive = false;
            }
        }

        this.loadCarrierServicesByDocumentType();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getCarriers(undefined, undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: CarrierDto[]) => {
                this.carriers = response;
            });
    }

    public onEditCanceling(event: any) {
        this.Editing = false;
    }

    public handleValueChange(event: any): void {
        const previousValue = event.previousValue;
        const newValue = event.value;
        const id = event.element.id;

        if (newValue) {
            let newCarrierService = new CarrierServiceDto();
            newCarrierService.id = 0;
            newCarrierService.serviceId = id;
            newCarrierService.carrierId = this.carriersChanges.id;

            if (this.carriersChanges.services == null) {
                let newServices: CarrierServiceDto[] = [];
                this.carriersChanges.services = newServices;
            }

            this.carriersChanges.services.push(newCarrierService);
        } else {
            const indexOfObject = this.carriersChanges.services.findIndex((object) => {
                return object.serviceId === parseInt(id);
            });

            if (indexOfObject !== -1) {
                this.carriersChanges.services.splice(indexOfObject, 1);
            }
        }
    }

    public onSavingCarrier(e: any): void {
        const change = e.changes[0];

        if (change || this.Editing) {
            e.cancel = true;

            if (change != undefined && change.type === 'remove') {
                // this._catalogService.delete(item.id).subscribe(() => {
                //   abp.notify.success(this.l('SuccessfullyDeleted'));
                //   this.refresh();
                // });
                abp.notify.success(this.l('SuccessfullyDeleted'));
            } else if ((change != undefined && change.type === 'insert') || this.Editing) {
                let allSchedulerAppointments: SchedulerAppointment[] = this.scheduler.instance.getDataSource().items();
                allSchedulerAppointments.forEach((appointment: SchedulerAppointment) => {
                    let nonWorkDaySelected: CarrierNonWorkingDayDto = new CarrierNonWorkingDayDto();
                    nonWorkDaySelected.nonWorkingDay = moment(appointment.startDate);

                    if (!this.carriersChanges.nonWorkingDays) {
                        this.carriersChanges.nonWorkingDays = [];
                    }
                    let nonWorkDayExists = this.carriersChanges.nonWorkingDays.find((i) => i.id === appointment.id);
                    if (!nonWorkDayExists) {
                        nonWorkDaySelected.isActive = true;
                        this.carriersChanges.nonWorkingDays.push(nonWorkDaySelected);
                    }
                });

                this.carriersChanges.nonWorkingDays.forEach((nonWorkingDayItem) => {
                    let foundItemIndex = allSchedulerAppointments.findIndex((item) => item.id === nonWorkingDayItem.id);
                    if (foundItemIndex < 0) {
                        nonWorkingDayItem.isActive = false;
                    }
                });

                this._catalogService
                    .createOrUpdateCarrier(this.carriersChanges)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.info(this.l('SavedSuccessfully'), this.l('Carriers'));
                        this.refresh();
                    });
            }

            e.changes = [];
            e.component.cancelEditData();
            this.Editing = false;
        }
    }

    isCustomPopupVisible: boolean = false;
    editAppointmentData: any = { text: 'Non Work Day', allDay: true } as any;
    public onAppointmentFormOpening(e: any): void {
        e.cancel = true;
        this.editAppointmentData = { ...e.appointmentData };
        this.editAppointmentData.endDate = this.editAppointmentData.startDate;

        //if (this.editAppointmentData.id) {
        this.isCustomPopupVisible = true;
        //}
        console.log('onAppointmentFormOpening', e);
    }

    public onHiding(e: any): void {
        this.editAppointmentData = {};
    }

    public saveDateSelected(): void {
        let allItems: SchedulerAppointment[] = this.scheduler.instance.getDataSource().items();
        const existsAppointment: SchedulerAppointment = allItems.find((e) => e.startDate == this.editAppointmentData.startDate);
        if (existsAppointment == null) {
            this.editAppointmentData.text = 'Non Work Day';
            this.editAppointmentData.allDay = true;
            this.scheduler.instance.addAppointment(this.editAppointmentData);
        }
        this.isCustomPopupVisible = false;
    }

    public onAppointmentDeleted(e: any) {
        console.log('onAppointmentDeleted', e, this.scheduler.instance.getDataSource().items());
    }

    public onDocumentTypeChanged(event: any): void {
        this.loadCarrierServicesByDocumentType();
    }

    private loadCarrierServicesByDocumentType() {
        this.servicesDataSource = new DataSource({
            store: new ArrayStore({
                key: 'serviceId',
                data: this.currentCarrierServices.filter(
                    (cs) =>
                        (cs.air && this.carriersChanges.documentTypeId === 1) ||
                        (cs.ground && this.carriersChanges.documentTypeId === 2) ||
                        (cs.sea && this.carriersChanges.documentTypeId === 3)
                ),
            }),
            sort: 'serviceName',
        });
    }
}
