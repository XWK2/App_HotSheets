import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { UserServiceProxy, SurveyServiceProxy, SurveyDto, UserByCurrentUserDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { Moment } from 'moment';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { exportDataGrid as exportDataGridToPdf } from 'devextreme/pdf_exporter';
import { jsPDF } from 'jspdf';
import { exportDataGrid } from 'devextreme/excel_exporter';

@Component({
    selector: 'app-surveys',
    templateUrl: './surveys.component.html',
    styleUrls: ['./surveys.component.css'],
    animations: [appModuleAnimation()],
})
export class SurveysComponent extends AppComponentBase implements OnInit {
    surveys: SurveyDto[] = [];
    isTableLoading: boolean = false;

    userIdSelected: number;
    usersDataSource: DataSource = new DataSource({
        store: [],
        pageSize: 50,
    });
    shippingCode: string;
    creationDate: Moment;
    qualificationOptions: any[] = [];
    qualificationSelected: string;

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    constructor(injector: Injector, private _surveyService: SurveyServiceProxy, private _userService: UserServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.qualificationOptions = [
            { id: 1, name: this.l('SurveyQuestion1Answer1') },
            { id: 2, name: this.l('SurveyQuestion1Answer2') },
            { id: 3, name: this.l('SurveyQuestion1Answer3') },
            { id: 4, name: this.l('SurveyQuestion1Answer4') },
        ];

        this._userService.getUsersByCurrentUser().subscribe((response: UserByCurrentUserDto[]) => {
            this.usersDataSource = new DataSource({
                store: response,
                paginate: true,
                pageSize: 50,
            });
        });

        this.refresh();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._surveyService
            .getSurveys(
                this.userIdSelected ?? undefined,
                this.shippingCode ?? undefined,
                this.creationDate ?? undefined,
                this.qualificationSelected ?? undefined
            )
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: SurveyDto[]) => {
                this.surveys = response;
            });
    }

    public exportGrid(e: any): void {
        if (e.format === 'xlsx') {
            const workbook = new Workbook();
            const worksheet = workbook.addWorksheet('Main sheet');
            exportDataGrid({
                worksheet: worksheet,
                component: e.component,
            }).then(function () {
                workbook.xlsx.writeBuffer().then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'SurveysReport_' + Date.now().toString() + '.xlsx');
                });
            });
            e.cancel = true;
        } else if (e.format === 'pdf') {
            const doc = new jsPDF();
            exportDataGridToPdf({
                jsPDFDocument: doc,
                component: e.component,
            }).then(() => {
                doc.save('SurveysReport_' + Date.now().toString() + '.pdf');
            });
        }
    }
}
