import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, CustomerDto, CustomerPlantContactDto, CustomerPlantDto } from '@shared/service-proxies/service-proxies';
import { DxFormComponent } from 'devextreme-angular';
import { ValidationResult } from 'devextreme/ui/validation_group';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { GridChange } from '../../../shared/models/grid-change';

@Component({
    selector: 'app-customer-edit',
    templateUrl: './customer-edit.component.html',
    styleUrls: ['./customer-edit.component.css'],
    animations: [appModuleAnimation()],
})
export class CustomerEditComponent extends AppComponentBase implements OnInit {
    customerId: number;
    customerEdit: CustomerDto = new CustomerDto();
    isTableLoading: boolean = false;

    isTablePlantsLoading: boolean = false;
    editRowKey?: number = null;
    changes: GridChange<CustomerPlantDto>[] = [];

    @ViewChild(DxFormComponent, { static: false }) formGeneralInfo: DxFormComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _routeParams: ActivatedRoute, private _router: Router) {
        super(injector);
    }

    public ngOnInit(): void {
        this.loadInfo();
    }

    public loadInfo(): void {
        this.customerId = this._routeParams.snapshot.params.id;
        if (this.customerId > 0) {
            this.isTableLoading = true;
            this._catalogService
                .getCustomerById(this.customerId)
                .pipe(
                    finalize(() => {
                        this.isTableLoading = false;
                    })
                )
                .subscribe((response: CustomerDto) => {
                    this.customerEdit = response;
                });
        } else {
            this.customerEdit.plants = [];
        }
    }

    public save(): void {
        let validate: ValidationResult = this.formGeneralInfo.instance.validate();
        if (validate.isValid) {
            let customerToSave: CustomerDto = cloneDeep(this.customerEdit);
            customerToSave.plants = [];

            this.customerEdit.plants?.forEach((originalPlantItem: CustomerPlantDto) => {
                if (isNaN(originalPlantItem.id)) {
                    originalPlantItem.id = undefined;
                }
                let plantItemToAdd: CustomerPlantDto = new CustomerPlantDto();
                Object.assign(plantItemToAdd, originalPlantItem);
                plantItemToAdd.customerId = this.customerEdit.id;
                plantItemToAdd.contacts = [];

                originalPlantItem.contacts?.forEach((originalContactItem: CustomerPlantContactDto) => {
                    if (isNaN(originalContactItem.id)) {
                        originalContactItem.id = undefined;
                    }
                    let contactItemToAdd: CustomerPlantContactDto = new CustomerPlantContactDto();
                    Object.assign(contactItemToAdd, originalContactItem);
                    contactItemToAdd.customerPlantId = plantItemToAdd.id;

                    plantItemToAdd.contacts.push(contactItemToAdd);
                });

                customerToSave.plants.push(plantItemToAdd);
            });

            this._catalogService
                .createOrUpdateCustomer(customerToSave)
                .pipe(
                    finalize(() => {
                        // this.saving = false;
                        // this.saveLabel = this.l('Save');
                    })
                )
                .subscribe(() => {
                    this.notify.success(this.l('SavedSuccessfully'), this.l('Customers'));
                    this.goToList();
                });
        }
    }

    public goToList(): void {
        this._router.navigate(['/app/catalogs/customers']);
    }

    public onSavingCustomerPlant(e: any): void {
        const change = e.changes[0];
        console.log('onSavingCustomerPlant', change, this.customerEdit);
        // if (change) {
        //     e.cancel = true;
        //     e.promise = this.processSaving(change);
        // }
    }

    async processSaving(change: GridChange<CustomerPlantDto>) {
        this.isTablePlantsLoading = true;
        console.log('processSaving', change);
        try {
            // if(change.type)
            // await this.service.saveChange(change);
            this.editRowKey = null;
            this.changes = [];
        } finally {
            this.isTablePlantsLoading = false;
        }
    }
}
