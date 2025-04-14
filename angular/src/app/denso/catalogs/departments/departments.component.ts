import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, DepartmentDto } from '@shared/service-proxies/service-proxies';
import { DxDataGridComponent } from 'devextreme-angular';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-departments',
    templateUrl: './departments.component.html',
    styleUrls: ['./departments.component.css'],
    animations: [appModuleAnimation()],
})
export class DepartmentsComponent extends AppComponentBase implements OnInit {
    departments: DepartmentDto[] = [];
    isTableLoading: boolean = false;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);

        this.onClosePopup = this.onClosePopup.bind(this);
        this.onSaveChanges = this.onSaveChanges.bind(this);
    }

    public ngOnInit(): void {
        this.refresh();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getDepartments(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: DepartmentDto[]) => {
                this.departments = response;
            });
    }

    public delete(item: any): void {
        abp.message.confirm(this.l('AreYouSureWantToDelete', item.id), this.l('AreYouSure'), (result: boolean) => {
            if (result) {
                // this._departmentService.delete(item.id).subscribe(() => {
                //   abp.notify.success(this.l('SuccessfullyDeleted'));
                //   this.refresh();
                // });
            }
        });
    }

    public onSave(event: any): void {
        if (event.changes && event.changes.length) {
            let item = event.changes[0];
            if (item.type && item.data) {
                let itemToCreateOrUpdate = this.departments.find((p) => p.id == item.key);
                if (itemToCreateOrUpdate && item.type === 'update') {
                    for (const [key, value] of Object.entries(item.data)) {
                        itemToCreateOrUpdate[key] = value;
                    }
                } else if (item.type === 'insert') {
                    itemToCreateOrUpdate = cloneDeep(item.data);                    
                }

                this._catalogService
                    .createOrUpdateDepartmentSp(itemToCreateOrUpdate)
                    .pipe(
                        finalize(() => {                           
                        })
                    )
                    .subscribe(() => {                        
                        this.notifySuccess(this.l('SavedSuccessfully'));
                        this.refresh();
                    });

                    // this._catalogService
                    // .createOrUpdateDepartment(itemToCreateOrUpdate)
                    // .pipe(
                    //     finalize(() => {
                    //         // this.saving = false;
                    //         // this.saveLabel = this.l('Save');
                    //     })
                    // )
                    // .subscribe(() => {
                    //     // this.notify.info(this.l('SavedSuccessfully'), this.l('Departments'));
                    //     this.notifySuccess(this.l('SavedSuccessfully'));
                    //     this.refresh();
                    // });
            }
        }
    }

    public onClosePopup(e: any): void {
        this.dataGrid.instance.cancelEditData();
    }

    public onSaveChanges(e: any): void {
        this.dataGrid.instance.saveEditData();
    }
}
