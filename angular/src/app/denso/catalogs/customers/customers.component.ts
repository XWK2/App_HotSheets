import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, CustomerDto } from '@shared/service-proxies/service-proxies';
import { DxDataGridComponent } from 'devextreme-angular';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { WsPortalShippingService } from '@app/denso/shared/services/ws-portal-shipping.service';

@Component({
    selector: 'app-customers',
    templateUrl: './customers.component.html',
    styleUrls: ['./customers.component.css'],
    animations: [appModuleAnimation()],
})
export class CustomersComponent extends AppComponentBase implements OnInit {
    customers: CustomerDto[] = [];
    isTableLoading: boolean = false;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    constructor(
        injector: Injector,
        private _catalogService: CatalogServiceProxy,
        private _router: Router,
        private _wsPortalShippingService: WsPortalShippingService
    ) {
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
            .getCustomers(undefined, undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: CustomerDto[]) => {
                this.customers = response;
            });
    }

    public onSave(event: any): void {
        // if (event.changes && event.changes.length) {
        //     let item = event.changes[0];
        //     if (item.type && item.data) {
        //         let itemToCreateOrUpdate = this.customers.find((p) => p.id == item.key);
        //         if (itemToCreateOrUpdate && item.type === 'update') {
        //             for (const [key, value] of Object.entries(item.data)) {
        //                 itemToCreateOrUpdate[key] = value;
        //             }
        //         } else if (item.type === 'insert') {
        //             itemToCreateOrUpdate = cloneDeep(item.data);
        //             itemToCreateOrUpdate.id = null;
        //         }
        //         this._catalogService
        //             .createOrUpdateCustomer(itemToCreateOrUpdate)
        //             .pipe(
        //                 finalize(() => {
        //                     // this.saving = false;
        //                     // this.saveLabel = this.l('Save');
        //                 })
        //             )
        //             .subscribe(() => {
        //                 this.notify.success(this.l('SavedSuccessfully'), this.l('Plants'));
        //                 this.refresh();
        //             });
        //     }
        // }
    }

    public onClosePopup(e: any): void {
        this.dataGrid.instance.cancelEditData();
    }

    public onSaveChanges(e: any): void {
        this.dataGrid.instance.saveEditData();
    }

    public createCustomer(): void {
        this._router.navigate(['/app/catalogs/customers/edit', -1]);
    }

    public editCustomer(item: any): void {
        this._router.navigate(['/app/catalogs/customers/edit', item.data.id]);
    }

    public wsPortalShippingUpdateCustomers(): void {
        let wsPortalHotSheetsUrls: string[] = [
            AppConsts.wsPortalHotSheetsUrl + '/UpdateCustomerShipping',
            AppConsts.wsPortalHotSheetsUrl + '/UpdateCustomerShiptoShipping',
        ];

        this._wsPortalShippingService.UpdateShippingInfo(wsPortalHotSheetsUrls, this.l('Customers'), this);
    }
}
