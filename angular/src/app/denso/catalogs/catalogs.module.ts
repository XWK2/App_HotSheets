import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { DensoSharedModule } from '@app/denso/shared/denso-shared.module';
import { DevExtremeSharedModule } from '@app/denso/shared/dev-extreme-shared.module';

import { CatalogsRoutingModule } from './catalogs-routing.module';
import { AppUtilsService } from '@shared/utils/app-utils.service';

//Components
import { PlantsComponent } from '@app/denso/catalogs/plants/plants.component';
import { DepartmentsComponent } from '@app/denso/catalogs/departments/departments.component';
import { DocumentTypesComponent } from '@app/denso/catalogs/document-types/document-types.component';
import { PartNumbersComponent } from '@app/denso/catalogs/part-numbers/part-numbers.component';
import { ProductCodesSatComponent } from '@app/denso/catalogs/product-codes-sat/product-codes-sat.component';
import { UnitMeasuresComponent } from '@app/denso/catalogs/unit-measures/unit-measures.component';
import { HotSheetTermsComponent } from '@app/denso/catalogs/hot-sheet-terms/hot-sheet-terms.component';
import { HotSheetReasonsComponent  } from '@app/denso/catalogs/hot-sheet-reasons/hot-sheet-reasons.component'
import { PaidByComponent } from '@app/denso/catalogs/paid-by/paid-by.component';
import { RmaAssignmentsComponent } from '@app/denso/catalogs/rma-assignment/rma-assignment.component';
import { CarriersComponent } from '@app/denso/catalogs/carriers/carriers.component';
import { CustomersComponent } from '@app/denso/catalogs/customers/customers.component';
import { CustomerEditComponent } from '@app/denso/catalogs/customers/customer-edit/customer-edit.component';
import { ServicesComponent } from '@app/denso/catalogs/services/services.component';
import { StaffComponent } from '@app/denso/catalogs/staff/staff.component';
import { HelpInfoComponent } from '@app/denso/catalogs/help-info/help-info.component';
import { EmployeesComponent } from '@app/denso/catalogs/employees/employees.component';
import { CurrenciesComponent } from '@app/denso/catalogs/currencies/currencies.component';
import { PartNumberPricesComponent } from '@app/denso/catalogs/part-number-prices/part-number-prices.component';
import { CustomerPlantContactsComponent } from '@app/denso/catalogs/customers/customer-plant-contacts/customer-plant-contacts.component';
//import { LogServicesComponent } from '@app/denso/catalogs/log-services/log-services.component';
import { PaymentTermsComponent } from '@app/denso/catalogs/payment-terms/payment-terms.component';
import { PartNumbersInternalComponent } from '@app/denso/catalogs/part-numbers-internal/part-numbers-internal.component';
import { PartNumberPricesInternalComponent } from '@app/denso/catalogs/part-number-prices-internal/part-number-prices-internal.component';
import { NoticesComponent } from '@app/denso/catalogs/notices/notices.component';

import { TransportModeComponent } from '@app/denso/catalogs/transport-mode/transport-mode.component';
import { StatusHotSheetComponent } from '@app/denso/catalogs/status-hot-sheet/status-hot-sheet.component';
import { ShortageShiftComponent } from '@app/denso/catalogs/shortage-shift/shortage-shift.component';

// Pipes
import { DatePipe } from '@angular/common';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        CatalogsRoutingModule,
        ServiceProxyModule,
        SharedModule,
        DensoSharedModule,
        DevExtremeSharedModule,
    ],
    declarations: [
        PlantsComponent,
        DepartmentsComponent,
        DocumentTypesComponent,
        PartNumbersComponent,
        ProductCodesSatComponent,
        UnitMeasuresComponent,
        HotSheetTermsComponent,
        HotSheetReasonsComponent,
        PaidByComponent,
        RmaAssignmentsComponent,
        CarriersComponent,
        CustomersComponent,
        ServicesComponent,
        StaffComponent,
        HelpInfoComponent,
        EmployeesComponent,
        CurrenciesComponent,
        PartNumberPricesComponent,
        CustomerEditComponent,
        CustomerPlantContactsComponent,
        //LogServicesComponent,
        PaymentTermsComponent,
        PartNumbersInternalComponent,
        PartNumberPricesInternalComponent,
        NoticesComponent,
        TransportModeComponent,
        StatusHotSheetComponent,
        ShortageShiftComponent,
    ],
    entryComponents: [
        // CreateOrEditSmartDialogComponent,
    ],
    providers: [AppUtilsService, [DatePipe]],
})
export class CatalogsModule {}
