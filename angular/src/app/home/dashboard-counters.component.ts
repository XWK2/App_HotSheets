import { Component, Injector, OnInit, Input } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
//import { DashboardItemData, HotSheetServiceProxy } from '@shared/service-proxies/service-proxies';
import { HotSheetServiceProxy } from '@shared/service-proxies/service-proxies';
import { NgxAnimatedCounterParams } from '@bugsplat/ngx-animated-counter';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CounterDetailsDialogComponent } from './counter-details-dialog.component';

// https://devexpress.github.io/web-dashboard-demo/
// https://github.com/DevExpress-Examples/dashboard-angular-app-get-started/tree/22.2.2%2B

@Component({
    selector: 'app-dashboard-counters',
    templateUrl: './dashboard-counters.component.html',
    animations: [appModuleAnimation()],
})
export class DashboardCountersComponent extends AppComponentBase implements OnInit {
    //@Input() items: DashboardItemData[] = [];

    animatedCounterParamsPendingApprovalByIE: NgxAnimatedCounterParams = {} as any;
    animatedCounterParamsPendingForApproval: NgxAnimatedCounterParams = {} as any;
    animatedCounterParamsPendingForProformaInvoice: NgxAnimatedCounterParams = {} as any;
    animatedCounterParamsPendingForPayment: NgxAnimatedCounterParams = {} as any;

    constructor(injector: Injector, private _modalService: BsModalService) {
        super(injector);
    }

    public ngOnInit(): void {}

    public reloadCounters(): void {
       /*  if (!this.items) {
            this.items = [];
        }
 */
        // this.animatedCounterParamsPendingApprovalByIE = this.getAnimatedCounterParams(this.getItemsTotalBy('pendingApprovalByIE'));
        // this.animatedCounterParamsPendingForApproval = this.getAnimatedCounterParams(this.getItemsTotalBy('pendingForApproval'));
        // this.animatedCounterParamsPendingForProformaInvoice = this.getAnimatedCounterParams(this.getItemsTotalBy('pendingForProformaInvoice'));
        // this.animatedCounterParamsPendingForPayment = this.getAnimatedCounterParams(this.getItemsTotalBy('pendingForPayment'));
    }

    private getAnimatedCounterParams(endNumber: number): NgxAnimatedCounterParams {
        let incrementValue: number = 20;
        if (endNumber >= 200) {
            incrementValue = 50;
        }
        return {
            start: 0,
            end: endNumber,
            interval: 100,
            increment: incrementValue,
        };
    }

    /* private getItemsTotalBy(propName: string): number {      
         return this.items.filter((i) => i[propName]).length; 
    } */

    public showMoreInfo(propName: string) {
       /*  if (this.items && this.items.length > 0) {
            this._modalService.show(CounterDetailsDialogComponent, {
                class: 'modal-xl',
                initialState: {
                    items: this.items.filter((i) => i[propName]),
                },
            });
        } */
    }
}
