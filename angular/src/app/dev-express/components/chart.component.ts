import { Component, Injector, ChangeDetectionStrategy, ViewChild, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppService, GrossProduct } from '../services/app.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  //animations: [appModuleAnimation()],
  styleUrls: ['./chart.component.css'],
  providers: [AppService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChartComponent extends AppComponentBase {
  grossProductData: GrossProduct[];
  
  constructor(injector: Injector, service: AppService) {
    super(injector);
    this.grossProductData = service.getGrossProductData();
  }  

  onPointClick(e) {
    e.target.select();
  }
}
