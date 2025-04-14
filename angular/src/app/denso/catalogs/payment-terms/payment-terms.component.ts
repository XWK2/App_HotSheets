import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, PaymentTermCarrierDto, PaymentTermDto } from '@shared/service-proxies/service-proxies';
import { DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-payment-terms',
    templateUrl: './payment-terms.component.html',
    styleUrls: ['./payment-terms.component.css'],
    animations: [appModuleAnimation()],
})
export class PaymentTermsComponent extends AppComponentBase implements OnInit {
    paymentTerms: PaymentTermDto[] = [];
    paymentTermEdit: PaymentTermDto;
    isTableLoading: boolean = false;
    saving: boolean = false;
    saveLabel: string;
    customersDataSource: DataSource;
    carriersDataSource: DataSource;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    paymentTermCarriers1: number[] = [];
    paymentTermCarriers2: number[] = [];

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);

        this.saveLabel = this.l('Save');
        this.onClosePopup = this.onClosePopup.bind(this);
        this.onSaveChanges = this.onSaveChanges.bind(this);
    }

    public ngOnInit(): void {
        this.isTableLoading = true;
        this.paymentTermEdit = new PaymentTermDto();
        Promise.all([this._catalogService.getCustomers(true, undefined).toPromise(), this._catalogService.getCarriers(true, undefined).toPromise()]).then(
            (responses) => {
                this.customersDataSource = new DataSource({
                    store: responses[0],
                    paginate: true,
                    pageSize: 50,
                });
                this.carriersDataSource = new DataSource({
                    store: responses[1],
                    paginate: true,
                    pageSize: 50,
                });

                this.refresh();
            }
        );
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getPaymentTerms(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: PaymentTermDto[]) => {
                this.paymentTerms = response;
            });
    }

    public onClosePopup(e: any): void {
        this.dataGrid.instance.cancelEditData();
    }

    public onSaveChanges(e: any): void {
        this.dataGrid.instance.saveEditData();
    }

    public onEditingStart(e: any): void {
        this.paymentTermEdit = cloneDeep(e.data);
        this.paymentTermCarriers1 = this.paymentTermEdit.carriers.filter((f) => f.warningType === 1).map((c) => c.carrierId);
        this.paymentTermCarriers2 = this.paymentTermEdit.carriers.filter((f) => f.warningType === 2).map((c) => c.carrierId);
    }

    public onSave(event: any): void {
        event.cancel = true;
        let dataToSave: PaymentTermDto = cloneDeep(this.paymentTermEdit);
        if (dataToSave) {
            if (event.changes[0] && event.changes[0].data) {
                for (const [key, value] of Object.entries(event.changes[0].data)) {
                    dataToSave[key] = value;
                }
            }
            dataToSave.carriers = [];
            this.paymentTermCarriers1.forEach((itemId) => {
                let paymentTermCarrierItem: PaymentTermCarrierDto = new PaymentTermCarrierDto();
                paymentTermCarrierItem.carrierId = itemId;
                paymentTermCarrierItem.paymentTermId = dataToSave.id;
                paymentTermCarrierItem.warningType = 1;
                dataToSave.carriers.push(paymentTermCarrierItem);
            });
            this.paymentTermCarriers2.forEach((itemId) => {
                let paymentTermCarrierItem: PaymentTermCarrierDto = new PaymentTermCarrierDto();
                paymentTermCarrierItem.carrierId = itemId;
                paymentTermCarrierItem.paymentTermId = dataToSave.id;
                paymentTermCarrierItem.warningType = 2;
                dataToSave.carriers.push(paymentTermCarrierItem);
            });

            this.saving = true;
            this.saveLabel = this.l('Saving');
            this._catalogService
                .createOrUpdatePaymentTerm(dataToSave)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                        this.saveLabel = this.l('Save');
                        this.refresh();
                    })
                )
                .subscribe(() => {
                    this.notify.success(this.l('SavedSuccessfully'), this.l('PaymentTerms'));
                    this.dataGrid.instance.cancelEditData();
                });
        }
    }
}
