import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, PlantDto } from '@shared/service-proxies/service-proxies';
import { DxDataGridComponent } from 'devextreme-angular';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-plants',
    templateUrl: './plants.component.html',
    styleUrls: ['./plants.component.css'],
    animations: [appModuleAnimation()],
})
export class PlantsComponent extends AppComponentBase implements OnInit {
    plants: PlantDto[] = [];
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
            .getPlants(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: PlantDto[]) => {
                this.plants = response;
            });
    }

    public onSave(event: any): void {
        if (event.changes && event.changes.length) {
            let item = event.changes[0];
            if (item.type && item.data) {
                let itemToCreateOrUpdate = this.plants.find((p) => p.id == item.key);
                if (itemToCreateOrUpdate && item.type === 'update') {
                    for (const [key, value] of Object.entries(item.data)) {
                        itemToCreateOrUpdate[key] = value;
                    }
                } else if (item.type === 'insert') {
                    itemToCreateOrUpdate = cloneDeep(item.data);
                    itemToCreateOrUpdate.id = null;
                }

                this._catalogService
                    .createOrUpdatePlant(itemToCreateOrUpdate)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('Plants'));
                        this.refresh();
                    });
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
