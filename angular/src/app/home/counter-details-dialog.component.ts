import { ChangeDetectionStrategy, Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef } from 'ngx-bootstrap/modal';
//import { DashboardItemData } from '@shared/service-proxies/service-proxies';
import { CryptoHelper } from '@shared/helpers/CryptoHelper';
import { Router } from '@angular/router';

@Component({
    selector: 'app-counter-details-dialog',
    templateUrl: './counter-details-dialog.component.html',
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CounterDetailsDialogComponent extends AppComponentBase implements OnInit {
    //items: DashboardItemData[] = [];
    constructor(injector: Injector, public bsModalRef: BsModalRef, private _router: Router) {
        super(injector);
    }

    public ngOnInit(): void {}

    public getIdEncrypted(shippingInstructionId: number): string {
        return CryptoHelper.encrypt(shippingInstructionId);
    }
}
