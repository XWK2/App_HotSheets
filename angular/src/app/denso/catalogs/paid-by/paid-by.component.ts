import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
    CatalogServiceProxy,
    PaidByDto,
    PaidByPaymentTermDto,
    PaidByHotSheetTermDto,
    PaymentTermDto,
    HotSheetTermDto,
} from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'paid-by',
    templateUrl: './paid-by.component.html',
    styleUrls: ['./paid-by.component.css'],
    animations: [appModuleAnimation()],
})
export class PaidByComponent extends AppComponentBase implements OnInit {
    paidPay: PaidByDto[] = [];
    paidPayEdit: PaidByDto;
    isTableLoading: boolean = false;

    paymentTerms: PaymentTermDto[] = [];
    hotSheetTerms: HotSheetTermDto[] = [];
    paymentTermsIds: number[] = [];
    hotSheetTermsIds: number[] = [];

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.isTableLoading = true;
        this.paidPayEdit = new PaidByDto();
        Promise.all([this._catalogService.getPaymentTerms(undefined).toPromise(), this._catalogService.getHotSheetTerms(undefined).toPromise()]).then(
            (responses) => {
                this.paymentTerms = responses[0];
                this.hotSheetTerms = responses[1];
                this.refresh();
            }
        );
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getPaidBy(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: PaidByDto[]) => {
                this.paidPay = response;
            });
    }

    public onSave(event: any): void {
        this._catalogService
            .createOrUpdatePaidBy(this.paidPayEdit)
            .pipe(
                finalize(() => {
                    // this.saving = false;
                    // this.saveLabel = this.l('Save');
                })
            )
            .subscribe(() => {
                this.notify.success(this.l('SavedSuccessfully'), this.l('PaidBy'));
                this.refresh();
            });
    }

    public onEditingStart(e: any): void {
        this.paidPayEdit = e.data;
        this.paymentTermsIds = this.paidPayEdit.paymentTerms.map((d) => d.paymentTermId);
        this.hotSheetTermsIds = this.paidPayEdit.hotSheetTerms.map((d) => d.hotSheetTermId);
    }

    public onSelectionChangedPaymentTerms(e: any): void {
        if (!this.paidPayEdit.paymentTerms) {
            this.paidPayEdit.paymentTerms = [];
        }
        if (e.addedItems && e.addedItems.length > 0) {
            e.addedItems.forEach((element) => {
                let paidByPaymentTermItem: PaidByPaymentTermDto = new PaidByPaymentTermDto();
                paidByPaymentTermItem.paidById = this.paidPayEdit.id;
                paidByPaymentTermItem.paymentTermId = element.id;

                this.paidPayEdit.paymentTerms.push(paidByPaymentTermItem);
            });
        }
        if (e.removedItems && e.removedItems.length > 0) {
            e.removedItems.forEach((element) => {
                const itemRemoveIndex: number = this.paidPayEdit.paymentTerms.findIndex((c) => c.paymentTermId == element.id);
                if (itemRemoveIndex >= 0) {
                    this.paidPayEdit.paymentTerms.splice(itemRemoveIndex, 1);
                }
            });
        }
        console.log(e);
    }

    public onSelectionChangedHotSheetTerms(e: any): void {
        if (!this.paidPayEdit.hotSheetTerms) {
            this.paidPayEdit.hotSheetTerms = [];
        }
        if (e.addedItems && e.addedItems.length > 0) {
            e.addedItems.forEach((element) => {
                let paidByShipmentTermItem: PaidByHotSheetTermDto = new PaidByHotSheetTermDto();
                paidByShipmentTermItem.paidById = this.paidPayEdit.id;
                paidByShipmentTermItem.hotSheetTermId = element.id;

                this.paidPayEdit.hotSheetTerms.push(paidByShipmentTermItem);
            });
        }
        if (e.removedItems && e.removedItems.length > 0) {
            e.removedItems.forEach((element) => {
                const itemRemoveIndex: number = this.paidPayEdit.hotSheetTerms.findIndex((c) => c.hotSheetTermId == element.id);
                if (itemRemoveIndex >= 0) {
                    this.paidPayEdit.hotSheetTerms.splice(itemRemoveIndex, 1);
                }
            });
        }
    }

    /*
    public onSave(event: any): void {
        if (event.changes && event.changes.length) {
            let item = event.changes[0];
            if (item.type && item.data) {
                let itemToCreateOrUpdate = this.paidPay.find((p) => p.id == item.key);
                if (itemToCreateOrUpdate && item.type === 'update') {
                    for (const [key, value] of Object.entries(item.data)) {
                        itemToCreateOrUpdate[key] = value;
                    }
                } else if (item.type === 'insert') {
                    itemToCreateOrUpdate = cloneDeep(item.data);
                    itemToCreateOrUpdate.id = null;
                }

                this._catalogService
                    .createOrUpdatePaidBy(itemToCreateOrUpdate)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('PaidBy'));
                        this.refresh();
                    });
            }
        }
    }
    */
}
