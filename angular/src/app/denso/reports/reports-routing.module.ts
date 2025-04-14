import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';
import { HotSheetsReportsComponent } from './report-hot-sheets/report-hot-sheets.component';

@NgModule({
    imports: [
        RouterModule.forChild([            
            {
                path: 'report-hot-sheets',
                component: HotSheetsReportsComponent,
                data: { permission: 'Pages.Reports.HotSheetsReports' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class ReportsRoutingModule {}
