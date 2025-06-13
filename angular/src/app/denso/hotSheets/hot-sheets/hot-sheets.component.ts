import { Component, Injector, OnInit, ViewChild,LOCALE_ID } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { UserServiceProxy, HotSheetServiceProxy, HotSheetsItemDto, HotSheetsDto,FileDto, TransportModeDto,CatalogServiceProxy, UserByCurrentUserDto, ShortageShiftDto, HotSheetsCommetsDto, GetHotSheetInput } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent,DxDataGridModule,DxButtonModule  } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { Moment } from 'moment';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { exportDataGrid as exportDataGridToPdf } from 'devextreme/pdf_exporter';
import { jsPDF } from 'jspdf';
import { exportDataGrid } from 'devextreme/excel_exporter';
import { FileUploader, FileUploaderOptions } from 'ng2-file-upload';
import { AppConsts } from '@shared/AppConsts';
import { TokenService } from 'abp-ng2-module';

import { DrawerProps } from '@app/denso/shared/models/drawer.props';
import { PopupTemplate } from '@app/denso/shared/models/popup-template';
import { cloneDeep, toInteger } from 'lodash-es';
import { DxAccordionComponent } from 'devextreme-angular';
import { AppUtilsService } from '@shared/utils/app-utils.service';
import * as moment from 'moment';
import { WsPortalShippingService } from '@app/denso/shared/services/ws-portal-shipping.service';

declare var bootstrap: any;

@Component({
    selector: 'hot-sheets',
    templateUrl: './hot-sheets.component.html',
    styleUrls: ['./hot-sheets.component.css'],
    animations: [appModuleAnimation()],
})
export class HotSheetsComponent extends AppComponentBase implements OnInit {
    hotSheets: HotSheetsItemDto[] = [];
    isTableLoading: boolean = false;
    groupingValues: any[];

    statusOptions = [        
        { text: 'Completed', value: 'Completed' },
        { text: 'Incomplete', value: 'Incomplete' }
      ];
    
    statusSelected: string = 'completado';

    //statusSelected: string | null = null;

    userIdSelected: number;
    usersDataSource: DataSource = new DataSource({
        store: [],
        pageSize: 50,
    });

    shippingCode: string;
    creationDate: Moment;
    qualificationOptions: any[] = [];
    qualificationSelected: string;

    plannerCode: string;
    supplierCode: string;
    partNumber: string;
    transportModeId: number ;
    statusId: number;
    shortageShiftId: number ;

    hotSheetChanges: HotSheetsDto = new HotSheetsDto();
    Editing: boolean = false;
    transportMode: TransportModeDto[] = [];
    shortageShift: ShortageShiftDto[] =[];
    isLoadingData: boolean = false;
    timeShortage: any;    

    hotSheetFiles: FileDto[] =[];

    hotSheetComments: HotSheetsCommetsDto[] =[];

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;


    //Files
    saving: boolean = false;
    //uploader: FileUploader;
    uploaderHotSheet: FileUploader;
    hotSheetEntityType: string = 'HotSheet';
    productFilesUploadedCount: number = 0;

    showDocumentViewerPopup: boolean = false;
    documentViewerExtensionsSupported: string[] = ['.jpg', '.jpeg', '.png', '.gif', '.pdf', '.ppt', '.pptx', '.doc', '.docx', '.xls', '.xlsx'];
    documentViewerFileSelected: FileDto;

    showCommentsPopup: boolean=false;
    filaSeleccionada: any = null;
    comentario: string = '';
    commentSelected: HotSheetsCommetsDto;

    startDate: Date;
    endDate: Date;

    constructor(
        injector: Injector, 
        private _hotSheetservice: HotSheetServiceProxy, 
        private _tokenService: TokenService, 
        private _userService: UserServiceProxy, 
        private _catalogService: CatalogServiceProxy,
        private _wsPortalShippingService: WsPortalShippingService) {
        super(injector);
        this.timeShortage = new Date();

        const uploaderOptions: FileUploaderOptions = {
            url: AppConsts.remoteServiceBaseUrl + '/api/services/app/File/Upload',
            autoUpload: false,
            authToken: 'Bearer ' + this._tokenService.getToken(),
            removeAfterUpload: true,
        };

        //this.uploader = new FileUploader(uploaderOptions);
        this.uploaderHotSheet = new FileUploader(uploaderOptions);

        /* this.uploader.onBuildItemForm = (item, form) => {
            form.append('entityType', this.hotSheetEntityType);
            form.append('entityId', this.hotSheetChanges.id);
        }; */

        this.uploaderHotSheet.onBuildItemForm = (item, form) => {
            form.append('entityType', this.hotSheetEntityType);
            form.append('entityId', this.hotSheetChanges.id);
        };

        // this.uploader.onCompleteAll = () => {
        //     this.saving = false;
        //     this.productFilesUploadedCount++;

        //     // if (this.productFilesUploadedCount === this.productFilesToUpload.length) {
        //     //     setTimeout(() => {
        //     //         abp.ui.clearBusy();
        //     //         this.showSaveNotification();
        //     //     }, 500);
        //     // }
        // };

        this.uploaderHotSheet.onCompleteAll = () => {
            this.saving = false;
            abp.ui.clearBusy();
            this.notify.success(this.l('SavedSuccessfully'), this.l('HotSheetFile'));
            this._hotSheetservice.getHotSheetFiles(this.hotSheetChanges.id).subscribe((filesReponse: FileDto[]) => {
                this.hotSheetFiles = filesReponse;
                this.hotSheetFiles.forEach((docItem) => {
                    docItem.url = AppConsts.remoteServiceBaseUrl + '/file/GetDocumentBy?docId=' + docItem.id;
                });
            });
        };  
    }

    public ngOnInit(): void {

        // if (this.statusOptions.length > 0) {
        //     this.statusSelected = this.statusOptions[0].value;
        // }

        this.refresh();

        this.groupingValues = [
            {
                value: 'withoutGrouping',
                text: this.l('NoFilter'),
            },
            {
                value: 'plannerName',
                text: `${this.l('FilteredBy')} ${this.l('Planner')}`,
            },
            {
                value: 'supplierName',
                text: `${this.l('FilteredBy')} ${this.l('Supplier')}`,
            },
            {
                value: 'partNumber',
                text: `${this.l('FilteredBy')} ${this.l('PartNumber')}`,
            },            
            {
                value: 'realShortageDate',
                text: `${this.l('FilteredBy')} ${this.l('RealShortageDate')}`,
            },
            {
                value: 'deliveryOrder',
                text: `${this.l('FilteredBy')} ${this.l('DeliveryOrder')}`,
            },
            // {
            //     value: 'creatorFullName',
            //     text: `${this.l('FilteredBy')} ${this.l('RequestedBy')}`,
            // },
            {
                value: 'asn',
                text: `${this.l('FilteredBy')} ${this.l('ASN')}`,
            }           
        ];


        this.isLoadingData = true;
        Promise.all([
            this._catalogService.getTransportMode(undefined).toPromise(),
            this._catalogService.getShortageShift(undefined).toPromise(),     
            
        ]).then((responses) => {
            this.transportMode = responses[0];
            this.shortageShift = responses[1];           

            this.isLoadingData = false;
        });

        
    }

    public onInitNewRow(event: any): void {
        this.hotSheetChanges = new HotSheetsDto();
    }

    public onEditingStart(event: any): void {
            this.hotSheetChanges = new HotSheetsDto();
            let newhotSheet = new HotSheetsItemDto(); 
            newhotSheet = this.hotSheets.find((pn: HotSheetsItemDto) => pn.hotSheetId === event.data.hotSheetId);
            this.hotSheetChanges.id = event.data.hotSheetId;
            this.hotSheetChanges.transportModeId = newhotSheet.transportModeId;            
            this.hotSheetChanges.deliveryOrder = newhotSheet.deliveryOrder;
            this.hotSheetChanges.trafficContainerFX = newhotSheet.trafficContainerFX;
            this.hotSheetChanges.unitNumber = newhotSheet.unitNumber;
            this.hotSheetChanges.etaDNMX = newhotSheet.etaDNMX;
            this.hotSheetChanges.shortageShiftId = newhotSheet.shortageShiftId;                                    
            //this.hotSheetChanges.realShortageDate = newhotSheet.realShortageDate;                
            this.hotSheetChanges.shortage = newhotSheet.shortage;
            
            // const shortage = newhotSheet.shortage;            
            // const shortageVal = newhotSheet.shortageVal;  
            // const parts = shortageVal.split(":");
            // const hora = parseInt(parts[0].toString());
            // const min =parseInt(parts[1].toString());
            // const seg = parseInt(parts[2].toString());

            const ahora = new Date();
            const hora = parseInt(this.agregarCeros(ahora.getHours()));
            const min = parseInt(this.agregarCeros(ahora.getMinutes()));
            const seg = parseInt(this.agregarCeros(ahora.getSeconds()));

            this.timeShortage = new Date(2025,1,1,hora,min,seg);
            //this.timeShortage = newhotSheet.shortageVal;
            //this.hotSheetChanges.shortage = new TimeSpan(this.timeShortage);

            this.hotSheetChanges.pcComments = newhotSheet.pcComments;
            this.Editing = true;

            Promise.all([
                this._hotSheetservice.getHotSheetFiles(event.data.hotSheetId).toPromise(),
                this._hotSheetservice.getHotSheetComments(event.data.hotSheetId).toPromise(),
            ]).then((responses) => {
                this.hotSheetFiles = responses[0];                
                this.hotSheetFiles.forEach((docItem) => {
                    docItem.url = AppConsts.remoteServiceBaseUrl + '/file/GetDocumentBy?docId=' + docItem.id;
                });

                this.hotSheetComments = responses[1];       
                this.isLoadingData = false;
            });
        }       
        
        agregarCeros(valor: number): string {
            return valor < 10 ? '0' + valor : valor.toString();
         }

        public onEditCanceling(event: any) {
            this.Editing = false;
        }


        public onSavingHotSheet(e: any): void {
                const change = e.changes[0];
        
                //veremos como viene el dato
                //var ok= this.timeShortage;

                if (change || this.Editing) {
                    e.cancel = true;
        
                    if (change != undefined && change.type === 'remove') {                      
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                    } else if ((change != undefined && change.type === 'insert') || this.Editing) {
                        this._hotSheetservice
                            .createOrUpdateHotSheet(this.hotSheetChanges)
                            .pipe(
                                finalize(() => {                                    
                                })
                            )
                            .subscribe(() => {
                                this.notify.success(this.l('SavedSuccessfully'), this.l('Hot Sheet'));
                                this.refresh();
                            });
                    }
        
                    e.changes = [];
                    e.component.cancelEditData();
                    this.Editing = false;
                }
        
                console.log('onSavingHotSheet', e, this.hotSheetChanges);
            }

    public refresh(): void {
        this.isTableLoading = true;
        let statusIncompleted = 0;
        // if(this.statusSelected == "Completed"){
        //     statusIncompleted = 1;
        // }

        let input: GetHotSheetInput = new GetHotSheetInput();
        input.startDate = this.startDate ? moment(this.startDate) : undefined;
        input.endDate = this.endDate ? moment(this.endDate) : undefined;
        input.statusHS = statusIncompleted;
        
        this._hotSheetservice
            .getHotSheets(input)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: HotSheetsItemDto[]) => {
                this.hotSheets = response;
               
            });
    }


    public groupChanged(e): void {
        this.dataGrid.instance.clearGrouping();
        if (e.value !== 'withoutGrouping') {
            this.dataGrid.instance.columnOption(e.value, 'groupIndex', 0);
        }

        //this.totalCount = this.getGroupCount(e.value);
    }
    
    searchValue: string = '';
    onSearch(e: any) {
        this.searchValue = e.value;
        this.dataGrid.instance.searchByText(this.searchValue); // Asegúrate de tener una referencia a tu grid
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
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'hotSheetsReport_' + Date.now().toString() + '.xlsx');
                });
            });
            e.cancel = true;
        } else if (e.format === 'pdf') {
            const doc = new jsPDF();
            exportDataGridToPdf({
                jsPDFDocument: doc,
                component: e.component,
            }).then(() => {
                doc.save('hotSheetsReport_' + Date.now().toString() + '.pdf');
            });
        }
    }

    public onReasonChanged(event: any): void {
        var ok =  event.value;
        const hours = '0' + ok.getHours().toString(); 
        const min = '0' + ok.getMinutes(); 
        const seg = '0' +  ok.getSeconds(); 
        const val = hours.slice(-2) + ':'+  min.slice(-2) + ':'+ seg.slice(-2);          

        this.hotSheetChanges.shortage = val;       
    }

    public downloadFile(fileItem: any): void {
        window.open(fileItem.url, '_blank');
    }

    public viewFile(fileItem: any): void {
        this.showDocumentViewerPopup = true;
        this.documentViewerFileSelected = fileItem;
    }

    public saveDocuments(): void {
        abp.ui.setBusy();
        this.saving = true;
        this.uploaderHotSheet.uploadAll();
    }

    public deleteFile(fileItem: any): void {
        abp.message.confirm(this.l('AreYouSureWantToDelete', fileItem.name), this.l('AreYouSure'), (answerYes: boolean) => {
            if (answerYes) {
                this._hotSheetservice.deleteFile(fileItem.id).subscribe(() => {
                    this.notify.success(this.l('SuccessfullyDeleted'), this.l('HotSheetFile'));
                    this.hotSheetFiles = this.hotSheetFiles.filter((f) => f.id !== fileItem.id);
                });
            }
        });
    }

    public get enabledEditInfo(): boolean {
        return (
            this.appSession.user.isPC || this.appSession.user.isPC            
        );
    }



    // public getActionOptionsBy(item: HotSheetsItemDto): any {
    //     let actionSettings: any[] = [];

    //     if (!item.isTemplate) {
    //         if (item.isOwner) {
    //             if (this.isGranted('Pages.ShippingInstructions.Edit')) {
    //                 if (item.statusId === DensoDocumentStatus.Draft) {
    //                     actionSettings.push({ value: 'edit', name: this.l('Edit'), icon: 'edit' });
    //                 } else {
    //                     actionSettings.push({ value: 'view', name: this.l('View'), icon: 'doc' });
    //                     if (item.statusId !== DensoDocumentStatus.Approved) {
    //                         actionSettings.push({ value: 'reset', name: this.l('Reset'), icon: 'revert' });
    //                     }
    //                 }
    //             } else if (this.isGranted('Pages.ShippingInstructions')) {
    //                 actionSettings.push({ value: 'view', name: this.l('View'), icon: 'doc' });
    //             }

    //             if (this.isGranted('Pages.ShippingInstructions.Cancel') && item.statusId === DensoDocumentStatus.Draft) {
    //                 actionSettings.push({ value: 'cancel', name: this.l('Cancel'), icon: 'trash' });
    //             }
    //             if (
    //                 this.isGranted('Pages.ShippingInstructions.Edit') &&
    //                 item.statusId === DensoDocumentStatus.Draft &&
    //                 (item.managerApprovalId || item.accountingApprovalId || item.ieStaffId)
    //             ) {
    //                 actionSettings.push({ value: 'sendForApproval', name: this.l('SendForApproval'), icon: 'importselected' });
    //             }
    //             if (
    //                 this.isGranted('Pages.ShippingInstructions.ExportToAS400') &&
    //                 item.statusId === DensoDocumentStatus.Approved &&
    //                 item.exportedCigmaStatus === 3
    //             ) {
    //                 actionSettings.push({ value: 'exportToAs400', name: this.l('ExportToAS400'), icon: 'export' });
    //             }
    //             // if (this.isGranted('Pages.ShippingInstructions.Create') && item.isOwner) {
    //             //     actionSettings.push({ value: 'generateCopy', name: this.l('GenerateCopy'), icon: 'copy' });
    //             // }
    //         } else {
    //             if (this.isGranted('Pages.ShippingInstructions')) {
    //                 //if (item.statusId === DensoDocumentStatus.Approved || item.statusId === DensoDocumentStatus.Cancelled) {
    //                 actionSettings.push({ value: 'view', name: this.l('View'), icon: 'doc' });
    //                 //}
    //             }

    //             if (this.isGranted('Pages.ShippingInstructions.Approvals') && item.statusId === DensoDocumentStatus.PendingForApproval) {
    //                 if (item.managerApprovalId === this.appSession.userId && !item.managerIsApproved) {
    //                     actionSettings.push({ value: 'managerApproval', name: this.l('ManagerApproval'), icon: 'check' });
    //                 }
    //                 if (item.accountingApproverUserId === this.appSession.userId && !item.accountingIsApproved && item.managerIsApproved) {
    //                     actionSettings.push({ value: 'accountingApproval', name: this.l('AccountingApproval'), icon: 'check' });
    //                 }
    //                 if (
    //                     item.ieStaffApproverUserId === this.appSession.userId &&
    //                     !item.ieStaffIsApproved &&
    //                     (item.accountingIsApproved || (item.managerIsApproved && item.accountingApproverUserId == null))
    //                 ) {
    //                     actionSettings.push({ value: 'ieStaffApproval', name: this.l('IEStaffApproval'), icon: 'check' });
    //                 }
    //             }
    //         }
    //     } else {
    //         if (this.isGranted('Pages.ShippingInstructions.Edit') && item.statusId === DensoDocumentStatus.Draft) {
    //             actionSettings.push({ value: 'edit', name: this.l('Edit'), icon: 'edit' });
    //         }
    //     }

    //     return actionSettings;
    // }

    abrirComentarioModal(e: any) {
        this.filaSeleccionada = e.row.data;
        this.comentario = this.filaSeleccionada.comentario || '';
        const modalElement = document.getElementById('comentarioModal');
        if (modalElement) {
          const modal = new bootstrap.Modal(modalElement);
          modal.show();
        }
      }
    
      guardarComentario() {
        if (this.filaSeleccionada) {
          this.filaSeleccionada.comentario = this.comentario;
        }
        // Cerrar el modal manualmente
        const modalElement = document.getElementById('comentarioModal');
        if (modalElement) {
          bootstrap.Modal.getInstance(modalElement)?.hide();
        }
        this.comentario = '';
        this.filaSeleccionada = null;
      }

      public viewComent(hotSheet: any): void {
        this.showCommentsPopup = true;
        var newCommentsItem = new HotSheetsCommetsDto();    
        newCommentsItem.hotSheetId = hotSheet.row.data.hotSheetId;
        //newCommentsItem.comments = hotSheet.row.data.pcComments;
        newCommentsItem.comments = "";
        this.commentSelected = newCommentsItem;
    }

    private coloresPorFecha = new Map<string, string>();

    // personalizarFila(e: any): void {
    //     if (e.rowType === 'data') {
    //       const fechaCompleta = new Date(e.data.creationDate);
    //       const fechaClave = fechaCompleta.toISOString().split('T')[0];
      
    //       if (!this.coloresPorFecha.has(fechaClave)) {
    //         const ahora = new Date();
    //         const fechaBase = new Date(fechaClave);
    //         const diasDiferencia = Math.floor((ahora.getTime() - fechaBase.getTime()) / (1000 * 60 * 60 * 24));
            
    //         let color: string;
      
    //         if (diasDiferencia < 5) {
    //           // Colores diferenciados para los primeros 5 días con gradientes más sutiles
    //           const coloresPrimeros5 = [
    //             'linear-gradient(45deg, #76b041, #a8d08d)', // Día 1 -> Verde suave
    //             'linear-gradient(45deg, #8bc34a, #c8e6c9)', // Día 2 -> Verde claro
    //             'linear-gradient(45deg, #ffeb3b, #fff59d)', // Día 3 -> Amarillo suave
    //             'linear-gradient(45deg, #ff9800, #ffe0b2)', // Día 4 -> Naranja suave
    //             'linear-gradient(45deg, #f44336,rgb(245, 116, 129))'  // Día 5 -> Rojo suave
    //           ];
    //           color = coloresPrimeros5[diasDiferencia];
    //         } else {
    //           // Gradiente de color para días posteriores
    //           const daysAgo = Math.min(diasDiferencia, 364);
    //           const hue = 120 - (120 * (daysAgo / 364)); // 120 → 0
    //           color = `linear-gradient(45deg, hsl(${hue}, 85%, 70%), hsl(${hue + 10}, 85%, 90%))`; // Gradiente suave
    //         }
      
    //         this.coloresPorFecha.set(fechaClave, color);
    //       }
      
    //       const colorAplicar = this.coloresPorFecha.get(fechaClave)!;
    //       e.rowElement.classList.remove('dx-row-alt');
    //       e.rowElement.style.setProperty('background', colorAplicar, 'important');
    //     }
    //   }
      
    personalizarFila(e: any): void {
        // if (e.rowType === 'data') {
        //   const fechaCompleta = new Date(e.data.creationDate);
        //   const fechaClave = fechaCompleta.toISOString().split('T')[0];
      
        //   if (!this.coloresPorFecha.has(fechaClave)) {
        //     const ahora = new Date();
        //     const fechaBase = new Date(fechaClave);
        //     const diasDiferencia = Math.floor((ahora.getTime() - fechaBase.getTime()) / (1000 * 60 * 60 * 24));
            
        //     let color: string;
      
        //     if (diasDiferencia < 5) {
        //       // Colores sólidos suaves para los primeros 5 días
        //       const coloresPrimeros5 = [
        //         //'#A8D08D', // Día 1 -> Verde claro
        //         '#fafbfd', // Día 1 -> Casi Blanco  
        //         '#FFF59D', // Día 3 -> Amarillo muy suave
        //         '#FFE0B2', // Día 4 -> Naranja suave
        //         '#FFCDD2',  // Día 5 -> Rojo suave
        //         '#f08989'  // Día 5 -> Naranja
        //       ];
        //       color = coloresPrimeros5[diasDiferencia];
        //     } else {           
        //         color = '#f08989' ;
        //     }
      
        //     this.coloresPorFecha.set(fechaClave, color);
        //   }
      
        //   const colorAplicar = this.coloresPorFecha.get(fechaClave)!;
        //   e.rowElement.classList.remove('dx-row-alt');
        //   e.rowElement.style.setProperty('background-color', colorAplicar, 'important');
        // }
      }


      getFlagColor(dateStr: string): string {
        const today = new Date();
        const date = new Date(dateStr);
      
        // Ajustar las horas para comparar solo las fechas
        today.setHours(0, 0, 0, 0);
        date.setHours(0, 0, 0, 0);
      
        const diffTime = today.getTime() - date.getTime();
        const diffDays = Math.floor(diffTime / (1000 * 3600 * 24));
          
        //         '#fafbfd', // Día 1 -> Casi Blanco  
        //         '#FFF59D', // Día 3 -> Amarillo muy suave
        //         '#FFE0B2', // Día 4 -> Naranja suave
        //         '#FFCDD2',  // Día 5 -> Rojo suave
        //         '#f08989'  // Día 5 -> Naranja

        if (diffDays < 1) return '#fafbfd';   // Hoy
        if (diffDays < 2) return '#FFF59D';   // Ayer
        if (diffDays < 3) return '#FFE0B2';   // Hace 2 días
        if (diffDays < 4) return '#FFCDD2';   // Hace 3 días
        return '#f08989';                      // Más de 4 días
      }


    //   onCellPrepared(e: any) {
    //     if (
    //       e.rowType === 'data' &&
    //       e.column.type === 'buttons' &&
    //       e.cellElement
    //     ) {
    //       // Busca el botón con el ícono de comentario dentro de la celda
    //       const commentButton = e.cellElement.querySelector('.dx-icon-comment');
    //       if (commentButton) {
    //         const existComment = e.data.existComment;
    //         if (existComment === 0) {
    //           commentButton.classList.add('comment-icon-gray');
    //         } else {
    //           commentButton.classList.add('comment-icon-colored');
    //         }
    //       }
    //     }
    //   }

    //   onCellPrepared(e: any) {
    //     if (e.rowType === 'data' && e.column.type === 'buttons' && e.cellElement) {
    //       const buttons = e.cellElement.querySelectorAll('.dx-icon-comment');
      
    //       buttons.forEach((commentButton: HTMLElement) => {
    //         // Limpia clases anteriores si las hubiera
    //         commentButton.classList.remove('comment-icon-gray', 'comment-icon-colored');
      
    //         // Aplica nueva clase según el valor
    //         if (e.data.ExistComment === 0) {
    //           commentButton.classList.add('comment-icon-gray');
    //         } else {
    //           commentButton.classList.add('comment-icon-colored');
    //         }
    //       });
    //     }
    //   }

    //(onCellPrepared)="onCellPrepared($event)"

    onCellPrepared(e: any) {
        if (e.rowType === 'data' && e.column.type === 'buttons' && e.cellElement) {
          const iconContainer = e.cellElement.querySelector('.dx-icon-comment')?.parentElement;
      
          // Asegura que existe el contenedor y que la estrella aún no ha sido añadida
          if (iconContainer && !iconContainer.querySelector('.dx-icon-star')) {
            if (e.data.existComment >= 1) {
              const starIcon = document.createElement('i');
              starIcon.classList.add('dx-icon', 'dx-icon-star', 'custom-star-icon');
              iconContainer.appendChild(starIcon);
            }
          }
        }
    }

    showCommentButtonExist(e: any): boolean {
        let visible = false;
        if (e.row?.data?.existComment >= 1){
            visible = true;
        }
        return visible; // o cualquier lógica que necesites
      }

      showCommentButtonNotExist(e: any): boolean {
        let visible = false;
        if (e.row?.data?.existComment == 0){
            visible = true;
        }
        return visible; // o cualquier lógica que necesites
      }

      
      public wsPortalShippingUpdateStarSheets(): void {
        let wsPortalShippingUrls: string[] = [AppConsts.wsPortalHotSheetsUrl + '/UpdateStarSheets'];

        this._wsPortalShippingService.UpdateShippingInfo(wsPortalShippingUrls, this.l('StarSheetInfo'), this);
    }

}
