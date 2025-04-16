import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
    CatalogServiceProxy,
    CurrencyDto,
    PartNumberInternalDto,
    PartNumberPriceDto,
    PartNumberPriceInternalDto,
} from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { AppConsts } from '@shared/AppConsts';
import { WsPortalShippingService } from '@app/denso/shared/services/ws-portal-shipping.service';

// https://js.devexpress.com/Demos/WidgetsGallery/Demo/DataGrid/RowEditingAndEditingEvents/Angular/Light/

@Component({
    selector: 'app-part-number-prices-internal',
    templateUrl: './part-number-prices-internal.component.html',
    styleUrls: ['./part-number-prices-internal.component.css'],
    animations: [appModuleAnimation()],
})
export class PartNumberPricesInternalComponent extends AppComponentBase implements OnInit {
    partNumberPrices: PartNumberPriceInternalDto[] = [];
    partNumberPriceChanges: PartNumberPriceInternalDto = new PartNumberPriceInternalDto();

    customersDataSource: DataSource;
    partNumbersDataSource: DataSource;
    currencies: CurrencyDto[] = [];

    isTableLoading: boolean = false;
    isLoadingData: boolean = false;
    isEditing: boolean = false;

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _wsPortalShippingService: WsPortalShippingService) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();

        this.isLoadingData = true;
        Promise.all([
            this._catalogService.getCustomers(undefined, undefined).toPromise(),
            this._catalogService.getPartNumbers(undefined).toPromise(),
            this._catalogService.getCurrencies().toPromise(),
        ]).then((responses) => {
            this.customersDataSource = new DataSource({
                store: responses[0],
                paginate: true,
                pageSize: 50,
            });

            this.partNumbersDataSource = new DataSource({
                store: responses[1],
                paginate: true,
                pageSize: 50,
            });

            this.currencies = responses[2];

            this.isLoadingData = false;
        });
    }

    public onInitNewRow(event: any): void {
        this.partNumberPriceChanges = new PartNumberPriceInternalDto();
    }

    public onEditingStart(event: any): void {
        this.partNumberPriceChanges = new PartNumberPriceInternalDto();
        let newPartNumberPrice = new PartNumberPriceInternalDto(); //cloneDeep(this.unitMeasureSatChanges);
        newPartNumberPrice = this.partNumberPrices.find((pn: PartNumberPriceInternalDto) => pn.id === event.data.id);
        this.partNumberPriceChanges.id = event.data.id;
        this.partNumberPriceChanges.customerId = newPartNumberPrice.customerId;
        this.partNumberPriceChanges.partNumberInternalId = newPartNumberPrice.partNumberInternalId;
        this.partNumberPriceChanges.unitPrice = newPartNumberPrice.unitPrice;
        this.partNumberPriceChanges.currency = newPartNumberPrice.currency;
        this.partNumberPriceChanges.publishDate = newPartNumberPrice.publishDate;
        this.partNumberPriceChanges.isActive = newPartNumberPrice.isActive;
        this.isEditing = true;
    }

    public onEditCanceling(event: any) {
        this.isEditing = false;
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getPartNumberPricesInternal(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: PartNumberPriceInternalDto[]) => {
                this.partNumberPrices = response;
            });
    }

    public onSavingPartNumber(e: any): void {
        const change = e.changes[0];

        if (change || this.isEditing) {
            e.cancel = true;

            if (change != undefined && change.type === 'remove') {
                // abp.notify.success(this.l('SuccessfullyDeleted'));
            } else if ((change != undefined && change.type === 'insert') || this.isEditing) {
                this._catalogService
                    .createOrUpdatePartNumberPriceInternal(this.partNumberPriceChanges)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('PartNumberPrices'));
                        this.refresh();
                    });
            }

            e.changes = [];
            e.component.cancelEditData();
            this.isEditing = false;
        }
    }

    public wsPortalShippingUpdatePrices(): void {
        let wsPortalHotSheetsUrls: string[] = [AppConsts.wsPortalHotSheetsUrl + '/UpdatePricesShipping'];

        this._wsPortalShippingService.UpdateShippingInfo(wsPortalHotSheetsUrls, this.l('PartNumberPricesInternal'), this);
    }
}
