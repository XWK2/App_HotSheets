import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';

import { DensoSharedModule } from '@app/denso/shared/denso-shared.module';

import { AdminRoutingModule } from './admin-routing.module';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { TreeModule } from 'primeng/tree';

import { DxDropDownBoxModule, DxTemplateModule } from 'devextreme-angular';
import { NgSelectModule } from '@ng-select/ng-select';

// roles
import { RolesComponent } from '@app/admin/roles/roles.component';
import { CreateRoleDialogComponent } from '@app/admin/roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from '@app/admin/roles/edit-role/edit-role-dialog.component';
import { PermissionTreeComponent } from '@app/admin/roles/permission-tree/permission-tree.component';

// users
import { UsersComponent } from '@app/admin/users/users.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { CreateUserDialogComponent } from '@app/admin/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/admin/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from '@app/admin/users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from '@app/admin/users/reset-password/reset-password.component';
import { UserDepartmentsPlantsComponent } from '@app/admin/users/user-departments-plants/user-departments-plants.component';
import { SettingsComponent } from '@app/admin/settings/settings.component';

import { ArrayToTreeConverterService } from '../../shared/utils/array-to-tree-converter.service';
import { TreeDataHelperService } from '@shared/utils/tree-data-helper.service';

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
} from 'devextreme-angular';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        BsDropdownModule,
        CollapseModule,
        TabsModule,
        AdminRoutingModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule,
        NgSelectModule,

        DxTemplateModule,
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
        DxDropDownBoxModule,
        TreeModule,
        DensoSharedModule,
    ],
    declarations: [
        // users
        UsersComponent,
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ChangePasswordComponent,
        ResetPasswordDialogComponent,
        UserDepartmentsPlantsComponent,
        // roles
        RolesComponent,
        CreateRoleDialogComponent,
        EditRoleDialogComponent,
        PermissionTreeComponent,
        SettingsComponent,
    ],
    entryComponents: [
        // users
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ResetPasswordDialogComponent,
        // // roles
        CreateRoleDialogComponent,
        EditRoleDialogComponent,
    ],
    providers: [ArrayToTreeConverterService, TreeDataHelperService],
})
export class AdminModule {}
