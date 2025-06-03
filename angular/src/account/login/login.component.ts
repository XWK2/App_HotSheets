import { Component, Injector } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppAuthService } from '@shared/auth/app-auth.service';

import {
  AuthenticateModel,
  AuthenticateResultModel,
  TokenAuthServiceProxy,  
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './login.component.html',
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase {
  submitting = false;
  
  //Aqui pondria el codigo.
  userName: string = '';

  constructor(
    injector: Injector,
    public authService: AppAuthService,
    private _sessionService: AbpSessionService,
    private _tokenAuthService: TokenAuthServiceProxy    
  ) {
    super(injector);
  }

  ngOnInit(): void {

    // this._tokenAuthService.getUserName().subscribe(name => {
    //   var ok = name;
    // }, error => {
    //   console.error('Error obteniendo nombre de usuario:', error);
    //   alert('error'); 
    // });

    this._tokenAuthService.authenticateLdap("","").subscribe(result => {
      var valido = result;
    }, error => {
      console.error('Error validacion de usuario:', error);
      alert('error'); 
    });

  }

  get multiTenancySideIsTeanant(): boolean {
    return this._sessionService.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this._sessionService.tenantId) {
      return false;
    }

    return true;
  }

  login(): void {
    this.submitting = true;
    this.authService.authenticate(() => (this.submitting = false));
  }
}
