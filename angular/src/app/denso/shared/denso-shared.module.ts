import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';

import { SharedModule } from '@shared/shared.module';
import { DevExtremeSharedModule } from '@app/denso/shared/dev-extreme-shared.module';

//Components
import { UserInfoPopoverComponent } from '@app/denso/shared/user-info-popover/user-info-popover.component';
import { AddressDetailsDrawerComponent } from '@app/denso/shared/address-details-drawer/address-details-drawer.component';
import { UnitMeasuresSelectComponent } from '@app/denso/shared/unit-measures-select/unit-measures-select.component';
import { SurveyPopupComponent } from '@app/denso/shared/survey-popup/survey-popup.component';

import { CommentPipe } from '@app/denso/shared/pipes/comment.pipe';

import { WsPortalShippingService } from '@app/denso/shared/services/ws-portal-shipping.service';

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, HttpClientJsonpModule, DevExtremeSharedModule, SharedModule],
    declarations: [UserInfoPopoverComponent, AddressDetailsDrawerComponent, UnitMeasuresSelectComponent, CommentPipe, SurveyPopupComponent],
    exports: [UserInfoPopoverComponent, AddressDetailsDrawerComponent, UnitMeasuresSelectComponent, CommentPipe, SurveyPopupComponent],
    entryComponents: [],
    providers: [WsPortalShippingService],
})
export class DensoSharedModule {}
