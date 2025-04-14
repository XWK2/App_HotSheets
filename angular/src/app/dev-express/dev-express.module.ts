import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import {
    DxMenuModule,
    DxRangeSelectorModule,
    DxPopupModule,
    DxChartModule,
    DxPieChartModule,
    DxVectorMapModule,
    DxDataGridModule,
    DxBulletModule,
    DxButtonModule,
    DxPivotGridModule
} from 'devextreme-angular';

// Components
import { DevExpressComponent } from './dev-express.component';
import { DataGridComponent } from './components/data-grid.component';
import { PivotGridComponent } from './components/pivot-grid.component';
import { ChartComponent } from './components/chart.component';

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        DxMenuModule,
        DxRangeSelectorModule,
        DxPopupModule,
        DxChartModule,
        DxPieChartModule,
        DxVectorMapModule,
        DxDataGridModule,
        DxBulletModule,
        DxButtonModule,
        DxPivotGridModule
    ],
    declarations: [
        DevExpressComponent,
        DataGridComponent,
        PivotGridComponent,
        ChartComponent
    ]
})
export class DevExpressModule {

}
