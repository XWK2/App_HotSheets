import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '../../shared/auth/auth-route-guard';

import { UsersComponent } from './users/users.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { RolesComponent } from './roles/roles.component';
import { SettingsComponent } from './settings/settings.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'users', component: UsersComponent, data: { permission: 'Pages.Administration.Users' }, canActivate: [AppRouteGuard] },
            { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Administration.Roles' }, canActivate: [AppRouteGuard] },
            { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] },                                   
            { path: 'settings', component: SettingsComponent, data: { permission: 'Pages.Administration.Settings' }, canActivate: [AppRouteGuard] },

        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule { }
