import { Component, Injector, ChangeDetectionStrategy, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import notify from 'devextreme/ui/notify';
//import { DashboardData, DashboardItemData, ShippingServiceProxy } from '@shared/service-proxies/service-proxies';
import { HotSheetServiceProxy } from '@shared/service-proxies/service-proxies';
import { DensoDocumentStatus } from '@shared/AppEnums';
import { NgxAnimatedCounterParams } from '@bugsplat/ngx-animated-counter';
import { DashboardCountersComponent } from './dashboard-counters.component';
import { ChartsDialogComponent } from './charts-dialog.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { DxChartTypes } from 'devextreme-angular/ui/chart';
import { finalize } from 'rxjs';

// https://devexpress.github.io/web-dashboard-demo/
// https://github.com/DevExpress-Examples/dashboard-angular-app-get-started/tree/22.2.2%2B
// https://js.devexpress.com/Angular/Demos/WidgetsGallery/Demo/DateBox/Overview/MaterialBlueLight/

@Component({
    templateUrl: './home.component.html',
    animations: [appModuleAnimation()],
    // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent extends AppComponentBase implements OnInit {
    //items: DashboardItemData[] = [];
    pendingByIEGrouped: PendingApprovalByIEItem[] = [];
    statusIndicatorSelected: string = 'ByHotSheetType';
    startDate: Date;
    endDate: Date;
    statusIndicatorChartData: StatusIndicatorChartItem[] = [];
    statusIndicatorDataItems: StatusIndicatorDataItem[] = [];
    statusIndicatorOptions: any[] = [];
    userData: UserData[];
    ieStaffResponsesPerformanceItems: IEStaffResponsesPerformanceItem[] = [];
    isDataLoading: boolean = false;

    @ViewChild(DashboardCountersComponent) dashboardCountersComponent: DashboardCountersComponent;

    constructor(injector: Injector, private _shippingService: HotSheetServiceProxy, private _modalService: BsModalService) {
        super(injector);
    }

    public ngOnInit(): void {
        this.statusIndicatorOptions = [
            { name: 'ByHotSheetType', label: 'By Hot Sheet Type' },
            { name: 'ByPaymentType', label: 'By Payment Type' },
            { name: 'ByStatusIE', label: 'By Status IE' },
            { name: 'ByPlant', label: 'By Status Plant' },
            { name: 'ByDepartment', label: 'By Department' },
            { name: 'ByReason', label: 'By Reason' },
        ];

        this.isDataLoading = true;
       /*  this._shippingService
            .getDashboardData(undefined)
            .pipe(
                finalize(() => {
                    this.isDataLoading = false;
                })
            )
            .subscribe((response: DashboardData) => {
                this.items = response.items;
                setTimeout(() => {
                    this.dashboardCountersComponent.reloadCounters();
                });

                let pendingApprovalByIE: DashboardItemData[] = this.items.filter((i) => i.pendingApprovalByIE);
                let groupedByIEPendingApproval: any = this.groupBy(pendingApprovalByIE, 'ieStaffName');
                let pendingApprovalByIEKeys: string[] = Object.keys(groupedByIEPendingApproval);

                this.pendingByIEGrouped = [];
                pendingApprovalByIEKeys.forEach((elementKey) => {
                    if (groupedByIEPendingApproval[elementKey]) {
                        this.pendingByIEGrouped.push({
                            ieStaffName: elementKey,
                            count: groupedByIEPendingApproval[elementKey].length,
                        } as PendingApprovalByIEItem);
                    }
                });

                let groupedByIeStaffName: any = this.groupBy(this.items, 'ieStaffName');
                let ieStaffNameByIEKeys: string[] = Object.keys(groupedByIeStaffName);

                this.ieStaffResponsesPerformanceItems = [];
                ieStaffNameByIEKeys.forEach((elementKey) => {
                    if (groupedByIeStaffName[elementKey]) {
                        const ieStaffDaysLateApprovalSum: number = groupedByIeStaffName[elementKey].reduce((a, b) => +a + +b.daysLateApproval, 0);

                        this.ieStaffResponsesPerformanceItems.push({
                            //ieStaffName: elementKey.replace('RODRIGUEZ', 'RDGZ').replace('HERNANDEZ', 'HDEZ').replace('RAMIREZ', 'RMZ').replace('BARRERA', 'BAR'),
                            ieStaffName: elementKey,
                            number: ieStaffDaysLateApprovalSum / groupedByIeStaffName[elementKey].length,
                        } as IEStaffResponsesPerformanceItem);
                    }
                });

                this.refresStatusIndicatorData();
            }); */
    }

    private groupBy(xs, key): any {
        return xs.reduce(function (rv, x) {
            (rv[x[key]] = rv[x[key]] || []).push(x);
            return rv;
        }, {});
    }

    public refresStatusIndicatorData(): void {
        let propNameToFilter: string;

        switch (this.statusIndicatorSelected) {
            case 'ByHotSheetType':
                propNameToFilter = 'documentType';
                break;
            case 'ByPaymentType':
                propNameToFilter = 'paymentTerm';
                break;
            case 'ByPlant':
                propNameToFilter = 'plantName';
                break;
            case 'ByDepartment':
                propNameToFilter = 'departmentName';
                break;
            case 'ByReason':
                propNameToFilter = 'shipmentReason';
                break;
            case 'ByStatusIE':
                propNameToFilter = 'ieStaffName';
                break;
            default:
                propNameToFilter = 'creatorUser';
                break;
        }

        /* let itemsFiltered: DashboardItemData[] = this.items;
        if (this.startDate && this.endDate) {
            itemsFiltered = this.items.filter((a) => {
                return a.creationDate.isBetween(this.startDate, this.endDate);
            });
        } */

       /*  let groupedByCustomFilter: any = this.groupBy(itemsFiltered, propNameToFilter);
        let groupedByCustomFilterKeys: string[] = Object.keys(groupedByCustomFilter);
 */
       /*  this.statusIndicatorChartData = [];
        groupedByCustomFilterKeys.forEach((elementKey) => {
            if (groupedByCustomFilter[elementKey]) {
                this.statusIndicatorChartData.push({ propName: elementKey, count: groupedByCustomFilter[elementKey].length } as StatusIndicatorChartItem);
            }
        }); */

      /*   this.statusIndicatorDataItems = [];
        groupedByCustomFilterKeys.forEach((elementKey) => {
            this.statusIndicatorDataItems.push({
                propName: elementKey,
                count: groupedByCustomFilter[elementKey].length,
                pendingForApproval: groupedByCustomFilter[elementKey].filter((dataItem: DashboardItemData) => [5].includes(dataItem.statusId)).length,
                completed: groupedByCustomFilter[elementKey].filter(
                    (dataItem: DashboardItemData) => dataItem.statusId === 3 && dataItem.exportedCigmaStatus === 2
                ).length,
                approved: groupedByCustomFilter[elementKey].filter((dataItem: DashboardItemData) => dataItem.statusId === 3).length,
            } as StatusIndicatorDataItem);
        }); */
    }

    public onRangeDatesChanged(e): void {
        this.startDate = e.value[0];
        this.endDate = e.value[1];
    }

    public showChartDetails(chartType: string) {
        this._modalService.show(ChartsDialogComponent, {
            class: 'modal-xl',
            initialState: {
                chartType: chartType,
                ieStaffResponsesPerformanceItems: this.ieStaffResponsesPerformanceItems,
                statusIndicatorChartData: this.statusIndicatorChartData,
                pendingApprovalByIE: this.pendingByIEGrouped,
            },
        });
    }
}

export class PendingApprovalByIEItem {
    ieStaffName: string;
    count: number;
}

export class StatusIndicatorChartItem {
    propName: string;
    count: number;
}

export class StatusIndicatorDataItem {
    propName: string;
    count: number;
    pendingForApproval: number;
    completed: number;
    approved: number;
}

export class UserData {
    age: string;
    number: number;
}

export class IEStaffResponsesPerformanceItem {
    ieStaffName: string;
    number: number;
}
