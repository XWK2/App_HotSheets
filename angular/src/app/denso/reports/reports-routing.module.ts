import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';
import { HotSheetsReportsComponent } from './report-hot-sheets/report-hot-sheets.component';
import { PurchaseOrdersReportsComponent} from './report-purchase-orders/report-purchase-orders.component';

@NgModule({
    imports: [
        RouterModule.forChild([            
            {
                path: 'report-hot-sheets',
                component: HotSheetsReportsComponent,
                data: { permission: 'Pages.Reports.HotSheetsReports' },
                canActivate: [AppRouteGuard],
            },
            {
                path: 'report-purchase-orders',
                component: PurchaseOrdersReportsComponent,
                data: { permission: 'Pages.Reports.PurchaseOrdersReports' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class ReportsRoutingModule {}
