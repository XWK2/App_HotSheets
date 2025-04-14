import { ChangeDetectionStrategy, Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { IEStaffResponsesPerformanceItem, PendingApprovalByIEItem, StatusIndicatorChartItem } from './home.component';

@Component({
    selector: 'app-charts-dialog',
    templateUrl: './charts-dialog.component.html',
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChartsDialogComponent extends AppComponentBase implements OnInit {
    chartType: string;
    pendingApprovalByIE: PendingApprovalByIEItem[] = [];
    statusIndicatorChartData: StatusIndicatorChartItem[] = [];
    ieStaffResponsesPerformanceItems: IEStaffResponsesPerformanceItem[] = [];
    dialogTitle: string;
    constructor(injector: Injector, public bsModalRef: BsModalRef, private _router: Router) {
        super(injector);
    }

    public ngOnInit(): void {
        if (this.chartType === 'pendingApprovalByIE') {
            this.dialogTitle = this.l('PendingApprovalByIE');
        } else if (this.chartType === 'ieStaffResponsesPerformance') {
            this.dialogTitle = this.l('IEStaffResponsesPerformance');
        } else if (this.chartType === 'statusIndicator') {
            this.dialogTitle = this.l('StatusIndicator');
        }
    }

    customizeTooltipPendingApprovalByIE = ({ value, argumentText }) => ({
        html: `<div>
            <div class='tooltip-header'>${argumentText}</div>
            <div class='tooltip-body'>Pending Approvals: ${value}</div>
            </div>`,
    });

    customizeTooltipResponsesPerformance = ({ value, argumentText }) => ({
        html: `<div>
            <div class='tooltip-header'>${argumentText}</div>
            <div class='tooltip-body'>Performance: ${value ? parseFloat(value).toFixed(2) : 0}</div>
            </div>`,
    });

    customizeTooltipStatusIndicator = ({ value, argumentText }) => ({
        html: `<div>
            <div class='tooltip-header'>${argumentText}</div>
            <div class='tooltip-body'>Count: ${value}</div>
            </div>`,
    });

    // customizeTooltip = ({ points, argumentText }) => ({
    //     html:
    //         `<div><div class='tooltip-header'>${argumentText}</div>` +
    //         "<div class='tooltip-body'><div class='series-name'>" +
    //         `<span class='top-series-name'>${points[0].seriesName}</span>` +
    //         ": </div><div class='value-text'>" +
    //         `<span class='top-series-value'>${points[0].valueText}</span>` +
    //         "</div><div class='series-name'>" +
    //         `<span class='bottom-series-name'>${points[1].seriesName}</span>` +
    //         ": </div><div class='value-text'>" +
    //         `<span class='bottom-series-value'>${points[1].valueText}</span>` +
    //         '% </div></div></div>',
    // });
}
