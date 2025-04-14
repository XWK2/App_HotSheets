import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';

import { PlantsComponent } from './plants/plants.component';
import { DepartmentsComponent } from './departments/departments.component';
import { DocumentTypesComponent } from './document-types/document-types.component';
import { PartNumbersComponent } from './part-numbers/part-numbers.component';
import { ProductCodesSatComponent } from './product-codes-sat/product-codes-sat.component';
import { UnitMeasuresComponent } from './unit-measures/unit-measures.component';
import { HotSheetTermsComponent } from './hot-sheet-terms/hot-sheet-terms.component';
import { HotSheetReasonsComponent } from './hot-sheet-reasons/hot-sheet-reasons.component';
import { PaidByComponent } from './paid-by/paid-by.component';
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
//import { LogServicesComponent } from '@app/denso/catalogs/log-services/log-services.component';
import { PaymentTermsComponent } from '@app/denso/catalogs/payment-terms/payment-terms.component';
import { PartNumbersInternalComponent } from '@app/denso/catalogs/part-numbers-internal/part-numbers-internal.component';
import { PartNumberPricesInternalComponent } from '@app/denso/catalogs/part-number-prices-internal/part-number-prices-internal.component';
import { NoticesComponent } from '@app/denso/catalogs/notices/notices.component';
import { StatusHotSheetComponent } from './status-hot-sheet/status-hot-sheet.component';
import { TransportModeComponent } from './transport-mode/transport-mode.component';
import { ShortageShiftComponent } from './shortage-shift/shortage-shift.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: 'plants',
                component: PlantsComponent,
                data: { permission: 'Pages.Catalogs.Plants' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'departments',
                component: DepartmentsComponent,
                data: { permission: 'Pages.Catalogs.Departments' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'document-types',
                component: DocumentTypesComponent,
                data: { permission: 'Pages.Catalogs.DocumentTypes' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'part-numbers',
                component: PartNumbersComponent,
                data: { permission: 'Pages.Catalogs.PartNumbers' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'product-codes-sat',
                component: ProductCodesSatComponent,
                data: { permission: 'Pages.Catalogs.ProductCodesSAT' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'status-hot-sheet',
                component: StatusHotSheetComponent,
                data: { permission: 'Pages.Catalogs.StatusHotSheet' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'transport-mode',
                component: TransportModeComponent,
                data: { permission: 'Pages.Catalogs.TransportMode' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'shortage-shift',
                component: ShortageShiftComponent,
                data: { permission: 'Pages.Catalogs.ShortageShift' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'unit-measures',
                component: UnitMeasuresComponent,
                data: { permission: 'Pages.Catalogs.UnitMeasures' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'hot-sheet-terms',
                component: HotSheetTermsComponent,
                data: { permission: 'Pages.Catalogs.ShipmentTerms' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'hot-sheet-reasons',
                component: HotSheetReasonsComponent,
                data: { permission: 'Pages.Catalogs.ShipmentReasons' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'paid-by',
                component: PaidByComponent,
                data: { permission: 'Pages.Catalogs.PaidBy' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'rma-assignment',
                component: RmaAssignmentsComponent,
                data: { permission: 'Pages.Catalogs.RMAAssignments' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'carriers',
                component: CarriersComponent,
                data: { permission: 'Pages.Catalogs.Carriers' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'customers',
                component: CustomersComponent,
                data: { permission: 'Pages.Catalogs.Customers' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'customers/edit/:id',
                component: CustomerEditComponent,
                data: { permission: 'Pages.Catalogs.Customers' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'services',
                component: ServicesComponent,
                data: { permission: 'Pages.Catalogs.Services' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'staff',
                component: StaffComponent,
                data: { permission: 'Pages.Catalogs.IEStaff' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'help-info',
                component: HelpInfoComponent,
                data: { permission: 'Pages.Catalogs.HelpInfo' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'employees',
                component: EmployeesComponent,
                data: { permission: 'Pages.Catalogs.Employees' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'currencies',
                component: CurrenciesComponent,
                data: { permission: 'Pages.Catalogs.Currencies' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'part-number-prices',
                component: PartNumberPricesComponent,
                data: { permission: 'Pages.Catalogs.PartNumberPrices' },
                canActivate: [AppRouteGuard],
            },
           /*  {
                path: 'log-services',
                component: LogServicesComponent,
                data: { permission: 'Pages.Catalogs.LogServices' },
                canActivate: [AppRouteGuard],
            }, */
            {
                path: 'payment-terms',
                component: PaymentTermsComponent,
                data: { permission: 'Pages.Catalogs.PaymentTerms' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'part-numbers-internal',
                component: PartNumbersInternalComponent,
                data: { permission: 'Pages.Catalogs.PartNumberPricesInternal' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'part-number-prices-internal',
                component: PartNumberPricesInternalComponent,
                data: { permission: 'Pages.Catalogs.PartNumberPricesInternal' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'notices',
                component: NoticesComponent,
                data: { permission: 'Pages.Catalogs.Notices' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class CatalogsRoutingModule {}
