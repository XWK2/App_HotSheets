import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';
import { HotSheetsComponent } from '../hotSheets/hot-sheets/hot-sheets.component';

@NgModule({
    imports: [
        RouterModule.forChild([            
            {
                path: '',
                component: HotSheetsComponent,
                data: { permission: 'Pages.HotSheets' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class HotSheetsRoutingModule {}
