import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../../shared/auth/auth-route-guard';
import { PurchaseOrdersComponent } from './purchase-orders/purchase-orders.component';

@NgModule({
    imports: [
        RouterModule.forChild([            
            {
                path: '',
                component: PurchaseOrdersComponent,
                data: { permission: 'Pages.PurchaseOrders' },
                canActivate: [AppRouteGuard],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class PurchaseOrdersRoutingModule {}
