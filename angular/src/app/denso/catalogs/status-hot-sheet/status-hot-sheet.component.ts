import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, StatusHotSheetDto } from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'status-hot-sheet',
    templateUrl: './status-hot-sheet.component.html',
    styleUrls: ['./status-hot-sheet.component.css'],
    animations: [appModuleAnimation()],
})
export class StatusHotSheetComponent extends AppComponentBase implements OnInit {
    statusHotSheet: StatusHotSheetDto[] = [];
    isTableLoading: boolean = false;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getStatusHotSheet(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: StatusHotSheetDto[]) => {
                this.statusHotSheet = response;
            });
    }

    public delete(item: any): void {
        abp.message.confirm(this.l('AreYouSureWantToDelete', item.id), this.l('AreYouSure'), (result: boolean) => {
            if (result) {
                // this._statusHotSheetervice.delete(item.id).subscribe(() => {
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
                let itemToCreateOrUpdate = this.statusHotSheet.find((p) => p.id == item.key);
                if (itemToCreateOrUpdate && item.type === 'update') {
                    for (const [key, value] of Object.entries(item.data)) {
                        itemToCreateOrUpdate[key] = value;
                    }
                } else if (item.type === 'insert') {
                    itemToCreateOrUpdate = cloneDeep(item.data);
                    itemToCreateOrUpdate.id = null;
                }

                this._catalogService
                    .createOrUpdateStatusHotSheet(itemToCreateOrUpdate)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('statusHotSheet'));
                        this.refresh();
                    });
            }
        }
    }
}
