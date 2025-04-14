import { Component, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
    ConfigurationServiceProxy,
    DensoAS400SettingsEditDto,
    DensoGeneralSettingsEditDto,
    DensoInterfacesSettingsEditDto,
    DensoSettingsEditDto,
    SettingsParametersDto,
    TenantEmailSettingsEditDto,
    TenantSettingsEditDto,
    TenantSettingsServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css'],
    animations: [appModuleAnimation()],
})
export class SettingsComponent extends AppComponentBase implements OnInit {
    settingsParameters: SettingsParametersDto[] = [];
    isTableLoading: boolean = false;

    densoSettings: DensoSettingsEditDto = new DensoSettingsEditDto();
    as400: DensoAS400SettingsEditDto = new DensoAS400SettingsEditDto();
    interfaces: DensoInterfacesSettingsEditDto = new DensoInterfacesSettingsEditDto();
    general: DensoGeneralSettingsEditDto = new DensoGeneralSettingsEditDto();
    isSaving: boolean = false;
    emailSettings: TenantEmailSettingsEditDto = new TenantEmailSettingsEditDto();

    sendingTestEmail: boolean = false;
    testEmailAddress: string;

    constructor(injector: Injector, private _configurationServiceProxy: ConfigurationServiceProxy, private _tenantSettingsService: TenantSettingsServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.loadSettings();
    }

    loadSettings() {
        this._tenantSettingsService.getAllSettings().subscribe((settingsResponse: TenantSettingsEditDto) => {
            this.densoSettings = settingsResponse.denso;
            this.emailSettings = settingsResponse.email;

            if (settingsResponse.denso.aS400) {
                this.as400 = settingsResponse.denso.aS400;
            }

            if (settingsResponse.denso.interfaces) {
                this.interfaces = settingsResponse.denso.interfaces;
            }

            if (settingsResponse.denso.general) {
                this.general = settingsResponse.denso.general;
            }
        });
    }

    public refresh(): void {
        this.isTableLoading = true;
    }

    public saveAS400(): void {
        this._tenantSettingsService
            .updateDensoAs400Settings(this.as400)
            .pipe(
                finalize(() => {
                    this.sendingTestEmail = false;
                    abp.notify.success(this.l('SavedSuccessfully'), 'AS400 Settings');
                })
            )
            .subscribe(() => {});
    }

    public sendTestEmail(): void {
        if (this.testEmailAddress) {
            this.sendingTestEmail = true;
            this._tenantSettingsService
                .testEmail(this.testEmailAddress)
                .pipe(
                    finalize(() => {
                        this.sendingTestEmail = false;
                        abp.notify.success(this.l('EmailSentSuccessfully'));
                    })
                )
                .subscribe(() => {});
        }
    }

    public saveInterfacesSettings(): void {
        this._tenantSettingsService
            .updateDensoInterfacesSettings(this.interfaces)
            .pipe(
                finalize(() => {
                    abp.notify.success(this.l('SavedSuccessfully'), 'Interfaces Settings');
                })
            )
            .subscribe(() => {});
    }

    public saveGeneralSettings(): void {
        this.isSaving = true;
        this._tenantSettingsService
            .updateDensoGeneralSettings(this.general)
            .pipe(
                finalize(() => {
                    this.isSaving = false;
                    abp.notify.success(this.l('SavedSuccessfully'), 'General Settings');
                })
            )
            .subscribe(() => {});
    }
}
