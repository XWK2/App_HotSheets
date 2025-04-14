import { Component, HostListener, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, EmployeeDto, EmployeeOptionsDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';
import { AppUtilsService } from '@shared/utils/app-utils.service';

@Component({
    selector: 'app-employees',
    templateUrl: './employees.component.html',
    styleUrls: ['./employees.component.css'],
    animations: [appModuleAnimation()],
})
export class EmployeesComponent extends AppComponentBase implements OnInit {
    employees: EmployeeDto[] = [];
    isTableLoading: boolean = false;
    employeeChanges: EmployeeDto = new EmployeeDto();
    employeeOptions: EmployeeOptionsDto = new EmployeeOptionsDto();

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    public screenWidth: number;
    public screenHeight: number;
    popupHeight: number = 850;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _appUtils: AppUtilsService) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();

        this._catalogService.getEmployeeOptions().subscribe((response: EmployeeOptionsDto) => {
            this.employeeOptions = response;
        });

        this.onSaveChanges = this.onSaveChanges.bind(this);
        this.onClosePopup = this.onClosePopup.bind(this);

        this.screenWidth = window.innerWidth;
        this.screenHeight = window.innerHeight;
        this.popupHeight = this._appUtils.getPopupHeightByScreen(this.screenHeight);
    }

    @HostListener('window:resize', ['$event'])
    onWindowResize() {
        this.screenWidth = window.innerWidth;
        this.screenHeight = window.innerHeight;
        setTimeout(() => {
            this.popupHeight = this._appUtils.getPopupHeightByScreen(this.screenHeight);
        });
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getEmployees()
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: EmployeeDto[]) => {
                this.employees = response;
            });
    }

    public onSaveChanges(e: any): void {
        this.dataGrid.instance.saveEditData();
    }

    public onInitNewRow(event: any): void {
        this.employeeChanges = new EmployeeDto();
        this.employeeChanges.extras = false;
        this.employeeChanges.supervisor = false;
        this.employeeChanges.subsidy = false;
        this.employeeChanges.positionLevel = false;
        this.employeeChanges.notRequiredAHE = false;
    }

    public onEditingStart(event: any): void {
        this.employeeChanges = new EmployeeDto();

        this.employeeChanges.extras = false;
        this.employeeChanges.supervisor = false;
        this.employeeChanges.subsidy = false;
        this.employeeChanges.positionLevel = false;
        this.employeeChanges.notRequiredAHE = false;

        let employeeFound: EmployeeDto = this.employees.find((pn: EmployeeDto) => pn.id === event.data.id);
        if (employeeFound) {
            this.employeeChanges.id = event.data.id;
            this.employeeChanges.name = employeeFound.name;
            this.employeeChanges.surnames = employeeFound.surnames;
            this.employeeChanges.densoEmployeeId = employeeFound.densoEmployeeId;
            this.employeeChanges.credential = employeeFound.credential;
            this.employeeChanges.rfc = employeeFound.rfc;
            this.employeeChanges.birthDate = employeeFound.birthDate;
            this.employeeChanges.nss = employeeFound.nss;
            this.employeeChanges.curp = employeeFound.curp;
            this.employeeChanges.departmentId = employeeFound.departmentId;
            this.employeeChanges.positionId = employeeFound.positionId;
            this.employeeChanges.typeId = employeeFound.typeId;
            this.employeeChanges.plantId = employeeFound.plantId;
            this.employeeChanges.entryDate = employeeFound.entryDate;
            this.employeeChanges.extras = employeeFound.extras;
            this.employeeChanges.notRequiredAHE = employeeFound.notRequiredAHE;
            this.employeeChanges.supervisor = employeeFound.supervisor;
            this.employeeChanges.subsidy = employeeFound.subsidy;
            this.employeeChanges.positionLevel = employeeFound.positionLevel;
            this.employeeChanges.addressLine1 = employeeFound.addressLine1;
            this.employeeChanges.addressLine2 = employeeFound.addressLine2;
            this.employeeChanges.addressLine3 = employeeFound.addressLine3;
            this.employeeChanges.addressLine4 = employeeFound.addressLine4;
            this.employeeChanges.emailAddress = employeeFound.emailAddress;
            this.employeeChanges.isActive = employeeFound.isActive;
        }
    }

    public onSaving(e: any): void {
        const change = e.changes[0];
        //console.log(e);

        // if (change) {
        //     e.cancel = true;
        //     if (change.type === 'insert' || change.type === 'update') {
        this._catalogService
            .createOrUpdateEmployee(this.employeeChanges)
            .pipe(
                finalize(() => {
                    // this.saving = false;
                    // this.saveLabel = this.l('Save');
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'), this.l('Employees'));
                this.refresh();
            });
        //     }

        //     e.changes = [];
        //     e.component.cancelEditData();
        // }
    }

    public onClosePopup(e: any): void {
        this.dataGrid.instance.cancelEditData();
    }
}
