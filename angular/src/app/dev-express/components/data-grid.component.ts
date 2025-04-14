import { Component, Injector, ChangeDetectionStrategy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Employee, EmployeesService } from '../services/employees.service';
import DataSource from 'devextreme/data/data_source';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-data-grid',
  templateUrl: './data-grid.component.html',
  //animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataGridComponent extends AppComponentBase {
  employees: Employee[] = [];

  dataSource: DataSource;
  collapsed = false;

  contentReady = (e) => {
    if (!this.collapsed) {
      this.collapsed = true;
      e.component.expandRow(['EnviroCare']);
    }
  };

  customizeTooltip = (pointsInfo) => ({ text: `${parseInt(pointsInfo.originalValue)}%` });

  constructor(injector: Injector, service: EmployeesService, dataService: DataService) {
    super(injector);
    this.employees = service.getEmployees();  
    this.dataSource = dataService.getDataSource();  
  }  
}
