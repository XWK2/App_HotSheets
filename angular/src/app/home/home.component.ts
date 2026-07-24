import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HotSheetServiceProxy, GetDashboardInput } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs';

import * as ExcelJS from 'exceljs';
import * as FileSaver from 'file-saver';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    animations: [appModuleAnimation()]
})
export class HomeComponent extends AppComponentBase implements OnInit {

    dashboard = {
        wh: [] as ChartItem[],
        ie: [] as ChartItem[],
        status: [] as StatusItem[]
    };

    startDate: string | null = null;
    endDate: string | null = null;

    dateRange: Date[] = [];
    isDataLoading = false;
    showCharts = true;
    animationConfig = {
        enabled: true,
        duration: 800,
        easing: 'easeOutCubic'
    };

    constructor(
        injector: Injector,
        private _shippingService: HotSheetServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        const today = new Date();
        const past = new Date();

        past.setMonth(today.getMonth() - 10);

        this.startDate = this.formatDate(past);
        this.endDate = this.formatDate(today);
        this.dateRange = [past, today];

        this.loadDashboard();
    }

    loadDashboard(): void {
        if (!this.startDate || !this.endDate) return;

        this.isDataLoading = true;

        const input = new GetDashboardInput({
            startDate: this.startDate,
            endDate: this.endDate
        });

        this._shippingService.getDashboard(input)
            .pipe(finalize(() => this.isDataLoading = false))
            .subscribe((res: any) => {

                 // 🔥 DESTRUYE
                this.showCharts = false;

                setTimeout(() => {

                    // 🔥 CARGA DATA
                    this.dashboard.wh = this.transformData(res.wh);
                    this.dashboard.ie = this.transformData(res.ie);
                    this.dashboard.status = res.status;

                    // 🔥 RECREA → AQUÍ SE DISPARA ANIMACIÓN
                    this.showCharts = true;

                }, 50);
            });
    }

    onRangeDatesChanged(e: any): void {
        if (e.value && e.value.length === 2) {
            this.dateRange = e.value;
            this.startDate = this.formatDate(e.value[0]);
            this.endDate = this.formatDate(e.value[1]);
            this.loadDashboard();
        }
    }

    formatDate(date: Date): string {
        const y = date.getFullYear();
        const m = ('0' + (date.getMonth() + 1)).slice(-2);
        const d = ('0' + date.getDate()).slice(-2);
        return `${y}-${m}-${d}`;
    }

    transformData(data: any[]): ChartItem[] {
        if (!data) return [];

        const order: any = { 'A+': 1, 'A': 2, 'B': 3, 'C': 4 };

        return data
            .sort((a, b) => {
                //if (a.supplierName < b.supplierName) return -1;
                //if (a.supplierName > b.supplierName) return 1;
                return (order[a.typeColor] || 99) - (order[b.typeColor] || 99);
            })
            .map(x => ({
                //label: `${x.supplierName} - ${x.total} - ${x.typeColor}`,
                label: `${x.total} - ${x.typeColor}`,
                total: Number(x.total),
                color: this.getColor(x.typeColor)
            }));
    }

    getColor(type: string): string {
        switch (type) {
             case 'A+':
            return '#d50000';

        case 'A':
            return '#ff1f1f';

        case 'B':
            return '#f68b38';

        case 'C':
            return '#4caf50';

        default:
            return '#bdbdbd';
        }
    }
    
    customizePoint = (pointInfo: any) => {
        return {
            color: pointInfo.data.color
        };
    };

    customizePieLabel = (arg: any) => `${arg.valueText}`;

    customizeTooltip = (arg: any) => {
        // separa el label
        const parts = arg.argumentText.split(' - ');

        //const proveedor = parts[0];
        //const cantidad = parts[1];
        //const categoria = parts[2];

        return {
            text:
               // `${proveedor} - ${cantidad} - ${categoria}\n` +
                `${parts[0]}: ${parts[1]}\n` +
                `${arg.percentText}`
        };
    };

    refresh(): void {
        this.loadDashboard();
    }

    exportChart(chartId: string): void {
        const element: any = document.getElementById(chartId);

        html2canvas(element).then(canvas => {
            const imgData = canvas.toDataURL('image/png');
            const pdf = new jsPDF();
            pdf.addImage(imgData, 'PNG', 10, 10, 190, 120);
            pdf.save(`${chartId}.pdf`);
        });
    }

    getTotal(data: any[]): number {
        if (!data) return 0;
        return data.reduce((sum, item) => sum + (item.total || 0), 0);
    }

    async exportToExcel(): Promise<void> {
        const workbook = new ExcelJS.Workbook();

        const sheetWH = workbook.addWorksheet('WH');
        sheetWH.columns = [
            { header: 'Proveedor', key: 'label', width: 40 },
            { header: 'Total', key: 'total', width: 10 }
        ];
        this.dashboard.wh.forEach(x => sheetWH.addRow(x));

        const sheetIE = workbook.addWorksheet('IE');
        sheetIE.columns = sheetWH.columns;
        this.dashboard.ie.forEach(x => sheetIE.addRow(x));

        const charts = ['chart-wh', 'chart-ie', 'chart-status'];

        for (let i = 0; i < charts.length; i++) {
            const element: any = document.getElementById(charts[i]);
            const canvas = await html2canvas(element);
            const image = canvas.toDataURL('image/png');

            const imageId = workbook.addImage({
                base64: image,
                extension: 'png'
            });

            const sheet = workbook.addWorksheet(`Chart ${i + 1}`);
            sheet.addImage(imageId, 'A1:J20');
        }

        const buffer = await workbook.xlsx.writeBuffer();
        const blob = new Blob([buffer]);
        FileSaver.saveAs(blob, 'Dashboard.xlsx');
    }

    exportToPDF(): void {
        const DATA: any = document.querySelector('.content');

        html2canvas(DATA).then(canvas => {
            const imgData = canvas.toDataURL('image/png');
            const pdf = new jsPDF();
            pdf.addImage(imgData, 'PNG', 10, 10, 190, 160);
            pdf.save('Dashboard.pdf');
        });
    }
}

type ChartItem = {
    label: string;
    total: number;
    color: string;
};

type StatusItem = {
    area: string;
    abierto: number;
    cerrado: number;
};