import { Component, Injector, ChangeDetectionStrategy, ViewChild, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Employee, EmployeesService } from '../services/employees.service';

import {
  DxPivotGridModule,
  DxPivotGridComponent,
  DxChartModule,
  DxChartComponent,
} from 'devextreme-angular';
import PivotGridDataSource from 'devextreme/ui/pivot_grid/data_source';
import { AppService } from '../services/app.service';

@Component({
  selector: 'app-pivot-grid',
  templateUrl: './pivot-grid.component.html',
  //animations: [appModuleAnimation()],
  styleUrls: ['./pivot-grid.component.css'],
  providers: [AppService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PivotGridComponent extends AppComponentBase implements AfterViewInit {
  @ViewChild(DxPivotGridComponent, { static: false }) pivotGrid: DxPivotGridComponent;
  @ViewChild(DxChartComponent, { static: false }) chart: DxChartComponent;

  pivotGridDataSource: any;
  
  constructor(injector: Injector, service: AppService) {
    super(injector);
    this.customizeTooltip = this.customizeTooltip.bind(this);

    this.pivotGridDataSource = {
      fields: [{
        caption: 'Region',
        width: 120,
        dataField: 'region',
        area: 'row',
        sortBySummaryField: 'Total',
      }, {
        caption: 'City',
        dataField: 'city',
        width: 150,
        area: 'row',
      }, {
        dataField: 'date',
        dataType: 'date',
        area: 'column',
      }, {
        groupName: 'date',
        groupInterval: 'month',
        visible: false,
      }, {
        caption: 'Total',
        dataField: 'amount',
        dataType: 'number',
        summaryType: 'sum',
        format: 'currency',
        area: 'data',
      }],
      store: service.getSales(),
    };
  }  

  ngAfterViewInit() {
    this.pivotGrid.instance.bindChart(this.chart.instance, {
      dataFieldsDisplayMode: 'splitPanes',
      alternateDataFields: false,
    });

    setTimeout(() => {
      const dataSource = this.pivotGrid.instance.getDataSource();
      dataSource.expandHeaderItem('row', ['North America']);
      dataSource.expandHeaderItem('column', [2013]);
    }, 0);
  }

  customizeTooltip(args) {
    return {
      html: `${args.seriesName} | Total<div class='currency'>${args.valueText}</div>`,
    };
  }
}
