import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';

import { SurveysComponent } from './surveys/surveys.component';
import { HotSheetsComponent } from '../hotSheets/hot-sheets/hot-sheets.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: SurveysComponent,
                data: { permission: 'Pages.Surveys' },
                canActivate: [AppRouteGuard],
            },
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
export class SurveysRoutingModule {}
