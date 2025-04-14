import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, UserServiceProxy, CurrencyDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
    selector: 'app-currencies',
    templateUrl: './currencies.component.html',
    styleUrls: ['./currencies.component.css'],
    animations: [appModuleAnimation()],
})
export class CurrenciesComponent extends AppComponentBase implements OnInit {
    currencies: CurrencyDto[] = [];
    isTableLoading: boolean = false;
    currencyChanges: CurrencyDto = new CurrencyDto();

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _userService: UserServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getCurrencies()
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: CurrencyDto[]) => {
                this.currencies = response;
            });
    }

    public onInitNewRow(event: any): void {
        this.currencyChanges = new CurrencyDto();
    }

    public onEditingStart(e: any): void {
        this.currencyChanges = new CurrencyDto();
        let currencyInfo: CurrencyDto = this.currencies.find((pn: CurrencyDto) => pn.id === e.data.id);
        this.currencyChanges.id = e.data.id;
        this.currencyChanges.name = currencyInfo.name;
        this.currencyChanges.code = currencyInfo.code;
        this.currencyChanges.densoCode = currencyInfo.densoCode;
        this.currencyChanges.isActive = currencyInfo.isActive;
    }

    public onSave(e: any): void {
        this._catalogService
            .createOrUpdateCurrency(this.currencyChanges)
            .pipe(finalize(() => {}))
            .subscribe(() => {
                this.notify.success(this.l('SavedSuccessfully'), this.l('Currencies'));
                this.refresh();
            });
    }
}
