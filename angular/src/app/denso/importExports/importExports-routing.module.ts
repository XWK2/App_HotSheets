import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';
import { ImportExportsComponent } from '../importExports/import-exports/import-exports.component';

@NgModule({
    imports: [
        RouterModule.forChild([            
            {
                path: '',
                component: ImportExportsComponent,
                data: { permission: 'Pages.ImportExports' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class ImportExportsRoutingModule {}
