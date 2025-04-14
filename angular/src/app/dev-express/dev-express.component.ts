import { Component, Injector, ChangeDetectionStrategy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-dev-express',
  templateUrl: './dev-express.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DevExpressComponent extends AppComponentBase {  
  constructor(injector: Injector) {
    super(injector);    
  }  
}
