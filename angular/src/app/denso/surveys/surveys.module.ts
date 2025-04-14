import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { DensoSharedModule } from '@app/denso/shared/denso-shared.module';
import { DevExtremeSharedModule } from '@app/denso/shared/dev-extreme-shared.module';

import { SurveysRoutingModule } from './surveys-routing.module';

//Components
import { SurveysComponent } from '@app/denso/surveys/surveys/surveys.component';

import {
    DxDataGridModule,
    DxButtonModule,
    DxTextBoxModule,
    DxTagBoxModule,
    DxValidatorModule,
    DxTextAreaModule,
    DxSelectBoxModule,
    DxTabsModule,
    DxAccordionModule,
    DxTabPanelModule,
    DxFormModule,
    DxPopoverModule,
    DxListModule,
    DxHtmlEditorModule,
    DxDateBoxModule,
    DxBoxModule,
    DxDrawerModule,
    DxRadioGroupModule,
    DxPopupModule,
    DxNumberBoxModule,
    DxSwitchModule,
    DxCheckBoxModule,
    DxTemplateModule,
} from 'devextreme-angular';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        SurveysRoutingModule,
        ServiceProxyModule,
        SharedModule,

        DxDataGridModule,
        DxButtonModule,
        DxDataGridModule,
        DxButtonModule,
        DxTextBoxModule,
        DxTagBoxModule,
        DxValidatorModule,
        DxTextAreaModule,
        DxSelectBoxModule,
        DxTabsModule,
        DxAccordionModule,
        DxTabPanelModule,
        DxFormModule,
        DxPopoverModule,
        DxListModule,
        DxHtmlEditorModule,
        DxDateBoxModule,
        DxBoxModule,
        DxDrawerModule,
        DxRadioGroupModule,
        DxPopupModule,
        DxNumberBoxModule,
        DxSwitchModule,
        DxCheckBoxModule,
        DxTemplateModule,

        DensoSharedModule,
        DevExtremeSharedModule,
    ],
    declarations: [SurveysComponent],
    entryComponents: [],
    providers: [],
})
export class SurversModule {}
