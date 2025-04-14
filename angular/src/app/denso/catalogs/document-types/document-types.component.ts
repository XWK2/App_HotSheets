import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, DocumentTypeDto } from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-document-types',
    templateUrl: './document-types.component.html',
    styleUrls: ['./document-types.component.css'],
    animations: [appModuleAnimation()],
})
export class DocumentTypesComponent extends AppComponentBase implements OnInit {
    documentTypes: DocumentTypeDto[] = [];
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
            .getDocumentTypes(undefined, undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: DocumentTypeDto[]) => {
                this.documentTypes = response;
            });
    }

    public delete(item: any) {
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
                let itemToCreateOrUpdate = this.documentTypes.find((p) => p.id == item.key);
                if (itemToCreateOrUpdate && item.type === 'update') {
                    for (const [key, value] of Object.entries(item.data)) {
                        itemToCreateOrUpdate[key] = value;
                    }
                } else if (item.type === 'insert') {
                    itemToCreateOrUpdate = cloneDeep(item.data);
                    itemToCreateOrUpdate.id = null;
                }

                this._catalogService
                    .createOrUpdateDocumentType(itemToCreateOrUpdate)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('DocumentTypes'));
                        this.refresh();
                    });
            }
        }
    }
}
