import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';
import { StarSheetsComponent } from '../starSheets/star-sheets/star-sheets.component';

@NgModule({
    imports: [
        RouterModule.forChild([            
            {
                path: '',
                component: StarSheetsComponent,
                data: { permission: 'Pages.StarSheets' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class StarSheetsRoutingModule {}
