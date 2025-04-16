import { Component, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
    CatalogServiceProxy,
    PartNumberDto,
    UnitMeasureDto,
    ProductCodeSATDto,
    CountryDto,
    PartNumberXlsxDto,
    PartNumberResXlsxDto,
} from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';
import { BsModalService } from 'ngx-bootstrap/modal';
import notify from 'devextreme/ui/notify';
import * as XLSX from 'xlsx';
import { AppConsts } from '@shared/AppConsts';
import { WsPortalShippingService } from '@app/denso/shared/services/ws-portal-shipping.service';

// https://js.devexpress.com/Demos/WidgetsGallery/Demo/DataGrid/RowEditingAndEditingEvents/Angular/Light/

@Component({
    selector: 'app-part-numbers',
    templateUrl: './part-numbers.component.html',
    styleUrls: ['./part-numbers.component.css'],
    animations: [appModuleAnimation()],
})
export class PartNumbersComponent extends AppComponentBase implements OnInit {
    @Input() partNumbers: PartNumberDto[] = [];
    @Output() onPartNumberChanged = new EventEmitter<PartNumberDto[]>();

    partNumberChanges: PartNumberDto = new PartNumberDto();
    unitMeasures: UnitMeasureDto[] = [];
    unitMeasuresSelected: UnitMeasureDto[] = [];
    productCodesSAT: ProductCodeSATDto[] = [];
    countries: CountryDto[] = [];

    listPartNumberXlsx: PartNumberXlsxDto[] = [];
    partNumberResXlsxDto: PartNumberResXlsxDto[] = [];

    isTableLoading: boolean = false;
    isLoadingData: boolean = false;
    Editing: boolean = false;

    exceltoJson = {};
    FileSelected: boolean = false;
    public data: any[];

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
    @ViewChild('inputFile') inputFile;
    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _wsPortalShippingService: WsPortalShippingService) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();

        this.isLoadingData = true;
        Promise.all([
            this._catalogService.getUnitMeasures(undefined).toPromise(),
            this._catalogService.getProductCodesSat(undefined, undefined).toPromise(),
            this._catalogService.getCountries().toPromise(),
        ]).then((responses) => {
            this.unitMeasures = responses[0];
            this.productCodesSAT = responses[1];
            this.countries = responses[2];

            this.isLoadingData = false;
        });
    }

    public onInitNewRow(event: any): void {
        this.partNumberChanges = new PartNumberDto();
    }

    public onEditingStart(event: any): void {
        this.partNumberChanges = new PartNumberDto();
        let newPartNumber = new PartNumberDto(); //cloneDeep(this.unitMeasureSatChanges);
        newPartNumber = this.partNumbers.find((pn: PartNumberDto) => pn.id === event.data.id);
        this.partNumberChanges.id = event.data.id;
        this.partNumberChanges.number = newPartNumber.number;
        this.partNumberChanges.description = newPartNumber.description;
        this.partNumberChanges.descriptionSpanish = newPartNumber.descriptionSpanish;
        this.partNumberChanges.unitMeasureId = newPartNumber.unitMeasureId;
        this.partNumberChanges.productCodeSATId = newPartNumber.productCodeSATId;
        this.partNumberChanges.originCountryId = newPartNumber.originCountryId;
        this.partNumberChanges.fraction = newPartNumber.fraction;
        this.partNumberChanges.isActive = newPartNumber.isActive;
        this.Editing = true;
    }

    public onEditCanceling(event: any) {
        this.Editing = false;
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getPartNumbers(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: PartNumberDto[]) => {
                this.partNumbers = response;
            });
    }

    public onSavingPartNumber(e: any): void {
        const change = e.changes[0];

        if (change || this.Editing) {
            e.cancel = true;

            if (change != undefined && change.type === 'remove') {
                // this._catalogService.delete(item.id).subscribe(() => {
                //   abp.notify.success(this.l('SuccessfullyDeleted'));
                //   this.refresh();
                // });
                abp.notify.success(this.l('SuccessfullyDeleted'));
            } else if ((change != undefined && change.type === 'insert') || this.Editing) {
                this._catalogService
                    .createOrUpdatePartNumber(this.partNumberChanges)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('PartNumbers'));
                        this.refresh();
                    });
            }

            e.changes = [];
            e.component.cancelEditData();
            this.Editing = false;
        }

        console.log('onSavingPartNumber', e, this.partNumberChanges);
    }

    public onUnitMeasureChanged(value: number): void {
        if (value) {
            this.partNumberChanges.unitMeasureId = value;
        }
    }

    public onSaveDataXlsx(event: any): void {
        //this.isTableLoading = true;
        this.FileSelected = false;
        console.log(this.exceltoJson);

        //Save Json
        var headers = this.exceltoJson['headers'];
        var listParts = this.exceltoJson['sheet1'];

        // validate headers
        if (headers['header1'].length === 8) {
            var col0 = headers['header1'][0];
            var col1 = headers['header1'][1];
            var col2 = headers['header1'][2];
            var col3 = headers['header1'][3];
            var col4 = headers['header1'][4];
            var col5 = headers['header1'][5];
            var col6 = headers['header1'][6];
            var col7 = headers['header1'][7];
            if (
                col0 === 'DNMX Part Number' &&
                col1 === 'Descripcion Inglés 1' &&
                col2 === 'Descripcion Español 3' &&
                col3 === 'CLAVE UM SEGROVE' &&
                col4 === 'SAT Product Code' &&
                col5 === 'Weight' &&
                col6 === 'Clave país Segrove' &&
                col7 === 'Fracción'
            ) {
                //process only if it has data
                if (listParts.length > 0) {
                    for (const item of listParts) {
                        try {
                            var PartNumber = String(item['DNMX Part Number']);
                            var DescriptionInglish = item['Descripcion Inglés 1'];
                            var DescriptionSpanish = item['Descripcion Español 3'];
                            var UnitMeasureId = item['CLAVE UM SEGROVE'];
                            var ProductCodeSATId = item['SAT Product Code'];
                            var OriginCountryId = String(item['Clave país Segrove']);
                            var Weight = item['Weight'];
                            var Fraction = String(item['Fracción']);

                            var itemListResponse = new PartNumberXlsxDto();
                            itemListResponse.partNumber = PartNumber;
                            itemListResponse.descriptionInglish = DescriptionInglish;
                            itemListResponse.descriptionSpanish = DescriptionSpanish;
                            itemListResponse.unitMeasureId = UnitMeasureId;
                            itemListResponse.productCodeSATId = ProductCodeSATId;
                            itemListResponse.originCountryId = OriginCountryId;
                            itemListResponse.weight = Weight;
                            itemListResponse.fraction = Fraction;
                            this.listPartNumberXlsx.push(itemListResponse);
                        } catch (exception) {
                            throw exception;
                        }
                    }
                }

                this._catalogService
                    .createOrUpdatePartNumberXLSX(this.listPartNumberXlsx)
                    .pipe(
                        finalize(() => {
                            //this.isTableLoading = false;
                        })
                    )
                    .subscribe((response: PartNumberResXlsxDto[]) => {
                        this.partNumberResXlsxDto = response;
                        var counter = String(this.partNumberResXlsxDto[0].counter);
                        var counterDel = String(this.partNumberResXlsxDto[0].counterDel);
                        var counterIns = String(this.partNumberResXlsxDto[0].counterIns);
                        var counterUpd = String(this.partNumberResXlsxDto[0].counterUpd);
                        var sMsgResult = 'Counter: ' + counter + ', Deleted: ' + counterDel + ', Inserted: ' + counterIns + ', Updated: ' + counterUpd;

                        this.notify.success(this.l(sMsgResult), this.l('PartNumbersInternal: SavedSuccessfully'));
                        this.refresh();
                    });
            } else {
                this.notify.error(this.l('The document structure is invalid, \n The headers do not match.'), this.l('Warning'));
            }
        } else {
            this.notify.error(this.l('The document structure is invalid'), this.l('Warning'));
        }

        //if Save successfully then
        this.reset();
    }

    reset() {
        //console.log(this.inputFile.nativeElement.files);
        this.inputFile.nativeElement.value = '';
        //console.log(this.inputFile.nativeElement.files);
    }

    onFileChange(event: any) {
        this.exceltoJson = {};
        let headerJson = {};
        /* wire up file reader */
        const target: DataTransfer = <DataTransfer>event.target;
        // if (target.files.length !== 1) {
        //   throw new Error('Cannot use multiple files');
        // }
        const reader: FileReader = new FileReader();
        reader.readAsBinaryString(target.files[0]);
        console.log('filename', target.files[0].name);
        this.exceltoJson['filename'] = target.files[0].name;
        reader.onload = (e: any) => {
            /* create workbook */
            const binarystr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary' });
            for (var i = 0; i < wb.SheetNames.length; ++i) {
                const wsname: string = wb.SheetNames[i];
                const ws: XLSX.WorkSheet = wb.Sheets[wsname];
                const data = XLSX.utils.sheet_to_json(ws); // to get 2d array pass 2nd parameter as object {header: 1}
                this.exceltoJson[`sheet${i + 1}`] = data;
                const headers = this.get_header_row(ws);
                headerJson[`header${i + 1}`] = headers;
                //  console.log("json",headers)
            }
            this.exceltoJson['headers'] = headerJson;
            console.log(this.exceltoJson);
            this.FileSelected = true;
        };
    }

    get_header_row(sheet) {
        var headers = [];
        var range = XLSX.utils.decode_range(sheet['!ref']);
        var C,
            R = range.s.r; /* start in the first row */
        /* walk every column in the range */
        for (C = range.s.c; C <= range.e.c; ++C) {
            var cell = sheet[XLSX.utils.encode_cell({ c: C, r: R })]; /* find the cell in the first row */
            // console.log("cell",cell)
            var hdr = 'UNKNOWN ' + C; // <-- replace with your desired default
            if (cell && cell.t) {
                hdr = XLSX.utils.format_cell(cell);
                headers.push(hdr);
            }
        }
        return headers;
    }

    public wsPortalShippingUpdateParts(): void {
        let wsPortalHotSheetsUrls: string[] = [AppConsts.wsPortalHotSheetsUrl + '/UpdatePartsShipping'];

        this._wsPortalShippingService.UpdateShippingInfo(wsPortalHotSheetsUrls, this.l('PartNumbersInternal'), this);
    }
}
