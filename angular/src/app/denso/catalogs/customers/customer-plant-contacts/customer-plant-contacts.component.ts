import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, CustomerPlantDto } from '@shared/service-proxies/service-proxies';
import { DxFormComponent } from 'devextreme-angular';

@Component({
    selector: 'app-customer-plant-contacts',
    templateUrl: './customer-plant-contacts.component.html',
    styleUrls: ['./customer-plant-contacts.component.css'],
    animations: [appModuleAnimation()],
})
export class CustomerPlantContactsComponent extends AppComponentBase implements OnInit {
    @Input() plantContacts: CustomerPlantDto[] = [];

    @ViewChild(DxFormComponent, { static: false }) formGeneralInfo: DxFormComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _routeParams: ActivatedRoute, private _router: Router) {
        super(injector);
    }

    public ngOnInit(): void {}
}
