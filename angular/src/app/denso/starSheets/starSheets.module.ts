import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ServiceProxyModule } from  '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { DensoSharedModule } from '@app/denso/shared/denso-shared.module';
import { DevExtremeSharedModule } from '@app/denso/shared/dev-extreme-shared.module';

import { FileUploadModule } from 'ng2-file-upload';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { ImageViewerModule } from 'ngx-image-viewer';
import { NgxDocViewerModule } from 'ngx-doc-viewer';

import { StarSheetsRoutingModule } from './starSheets-routing.module';

//Components
import { StarSheetsComponent } from '@app/denso/starSheets/star-sheets/star-sheets.component';

import { DocumentViewerPopupComponent } from '@app/denso/starSheets/document-viewer-popup/document-viewer-popup.component';

import { CommentsPopupComponent } from '@app/denso/starSheets/comments-popup/comments-popup.component';

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
        StarSheetsRoutingModule,
        ServiceProxyModule,
        SharedModule,

        FileUploadModule,
        ImageViewerModule,
        PdfViewerModule,
        NgxDocViewerModule,
        
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
    declarations: [DocumentViewerPopupComponent,StarSheetsComponent,CommentsPopupComponent],
    entryComponents: [],
    providers: [],
})
export class StarSheetsModule {}
