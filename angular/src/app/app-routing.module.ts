import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { DevExpressComponent } from 'app/dev-express/dev-express.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent, canActivate: [AppRouteGuard] },
                    { path: 'dev-express', component: DevExpressComponent, canActivate: [AppRouteGuard] },
                    {
                        path: 'admin',
                        loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule),
                    },
                    // {
                    //     path: 'shipping',
                    //     loadChildren: () => import('./denso/shipping/shipping.module').then((m) => m.ShippingModule),
                    // },
                    {
                        path: 'catalogs',
                        loadChildren: () => import('./denso/catalogs/catalogs.module').then((m) => m.CatalogsModule),
                    },
                    {
                        path: 'surveys',
                        loadChildren: () => import('./denso/surveys/surveys.module').then((m) => m.SurversModule),
                    },
                    {
                        path: 'hotSheets',
                        loadChildren: () => import('./denso/hotSheets/hotSheets.module').then((m) => m.HotSheetsModule),
                    },
                    {
                        path: 'reports',
                        loadChildren: () => import('./denso/reports/reports.module').then((m) => m.ReportsModule),
                    },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
