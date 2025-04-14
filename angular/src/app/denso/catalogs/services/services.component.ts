import { Component, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, ServiceDto } from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'services',
    templateUrl: './services.component.html',
    styleUrls: ['./services.component.css'],
    animations: [appModuleAnimation()],
})
export class ServicesComponent extends AppComponentBase implements OnInit {
    @Input() services: ServiceDto[] = [];
    @Output() onServicesChanged = new EventEmitter<ServiceDto[]>();

    isTableLoading: boolean = false;
    isEditing: boolean = false;

    serviceChanges: ServiceDto = new ServiceDto();
    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();
    }

    public onInitNewRow(event: any): void {
        this.serviceChanges = new ServiceDto();
    }

    public onEditingStart(event: any): void {
        this.serviceChanges = new ServiceDto();
        let serviceInfo: ServiceDto = new ServiceDto();
        serviceInfo = this.services.find((pn: ServiceDto) => pn.id === event.data.id);
        this.serviceChanges.id = event.data.id;

        this.serviceChanges.name = serviceInfo.name;
        this.serviceChanges.description = serviceInfo.description;
        this.serviceChanges.isNational = serviceInfo.isNational;
        this.serviceChanges.isInternational = serviceInfo.isInternational;
        this.serviceChanges.showHigestCostWarning = serviceInfo.showHigestCostWarning;
        this.serviceChanges.ground = serviceInfo.ground;
        this.serviceChanges.air = serviceInfo.air;
        this.serviceChanges.sea = serviceInfo.sea;
        this.serviceChanges.isActive = serviceInfo.isActive;

        this.isEditing = true;
    }

    public onEditCanceling(event: any) {
        this.isEditing = false;
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getServices(undefined, undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: ServiceDto[]) => {
                this.services = response;
            });
    }

    public onSavingServices(e: any): void {
        const change = e.changes[0];

        if (change || this.isEditing) {
            e.cancel = true;

            if (change != undefined && change.type === 'remove') {
            } else if ((change != undefined && change.type === 'insert') || this.isEditing) {
                this._catalogService
                    .createOrUpdateService(this.serviceChanges)
                    .pipe(finalize(() => {}))
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('Services'));
                        this.refresh();
                    });
            }

            e.changes = [];
            e.component.cancelEditData();
            this.isEditing = false;
        }
    }
}
