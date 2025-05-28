import { Component, Injector, OnInit, ViewChild,LOCALE_ID } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { UserServiceProxy, HotSheetServiceProxy, PurchaseOrdersDto, PurchaseOrdersItemDto, HotSheetsItemDto, HotSheetsDto,FileDto, TransportModeDto,CatalogServiceProxy, UserByCurrentUserDto, ShortageShiftDto, HotSheetsCommetsDto, GetHotSheetInput } from '@shared/service-proxies/service-proxies';
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

import notify from "devextreme/ui/notify";
import { confirm } from "devextreme/ui/dialog";

declare var bootstrap: any;

@Component({
    selector: 'purchase-orders',
    templateUrl: './purchase-orders.component.html',
    styleUrls: ['./purchase-orders.component.css'],
    animations: [appModuleAnimation()],
})
export class PurchaseOrdersComponent extends AppComponentBase implements OnInit {
    purchaseOrders: PurchaseOrdersItemDto[] = [];
    isTableLoading: boolean = false;
    groupingValues: any[];

    statusOptions = [        
        { text: 'Open', value: 0 },
        { text: 'Closed', value: 1 }
      ];
    
    statusSelected: string = 'completado';
   

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

    purchaseOrdersChanges: PurchaseOrdersDto = new PurchaseOrdersDto();
    Editing: boolean = false;
    transportMode: TransportModeDto[] = [];
    shortageShift: ShortageShiftDto[] =[];
    isLoadingData: boolean = false;
    timeShortage: any;    


    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;


    //Files
    saving: boolean = false;
    //uploader: FileUploader;
    uploaderHotSheet: FileUploader;
    hotSheetEntityType: string = 'HotSheet';
    productFilesUploadedCount: number = 0;


    showCommentsPopup: boolean=false;
    filaSeleccionada: any = null;
    comentario: string = '';    

    startDate: Date;
    endDate: Date;

    constructor(
        injector: Injector, 
        private _hotSheetservice: HotSheetServiceProxy, 
        private _tokenService: TokenService, 
        private _userService: UserServiceProxy, 
        private _catalogService: CatalogServiceProxy ) {
        super(injector);
        this.timeShortage = new Date();
   
        
    }

    public ngOnInit(): void {

       

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
                value: 'requiredDate',
                text: `${this.l('FilteredBy')} ${this.l('RequiredDate')}`,
            },
            // {
            //     value: 'statusPO',
            //     text: `${this.l('FilteredBy')} ${this.l('StatusPO')}`,
            // }            
        ];

        
    }

    public openNewPurchaseOrderPopup(): void {
        this.dataGrid.instance.addRow();
    }

    public onInitNewRow(event: any): void {
        this.purchaseOrdersChanges = new PurchaseOrdersDto();            
        this.Editing = true;
    }

    public onEditingPurchaseOrder(event: any): void {
            this.purchaseOrdersChanges = new PurchaseOrdersDto();
            let newpurchaseOrder = new PurchaseOrdersItemDto(); 
            newpurchaseOrder = this.purchaseOrders.find((pn: PurchaseOrdersItemDto) => pn.purchaseOrderId === event.data.purchaseOrderId);
            this.purchaseOrdersChanges.id = event.data.purchaseOrderId;
            this.purchaseOrdersChanges.plannerCode = newpurchaseOrder.plannerCode;            
            this.purchaseOrdersChanges.plannerName = newpurchaseOrder.plannerName;
            this.purchaseOrdersChanges.purchaseOrder = newpurchaseOrder.purchaseOrder;
            this.purchaseOrdersChanges.line = newpurchaseOrder.line;
            this.purchaseOrdersChanges.partNumber = newpurchaseOrder.partNumber;
            this.purchaseOrdersChanges.partDescription = newpurchaseOrder.partDescription;                                    
            this.purchaseOrdersChanges.supplierCode = newpurchaseOrder.supplierCode;                
            this.purchaseOrdersChanges.supplierName = newpurchaseOrder.supplierName;
            this.purchaseOrdersChanges.qty = newpurchaseOrder.qty;
            this.purchaseOrdersChanges.requiredDate = newpurchaseOrder.requiredDate;
            this.purchaseOrdersChanges.statusId = newpurchaseOrder.statusId;
            this.purchaseOrdersChanges.ticket = newpurchaseOrder.ticket;          
            this.Editing = true;
            
        }        

        public onEditCanceling(event: any) {
            this.Editing = false;
        }

        deletePurchaseOrder = (e: any) => {             
            e.cancel = true;                          
            confirm("¿Estás seguro de que deseas eliminar este registro?", "Confirmar eliminación")
            .then((result) => {
                if (result) {
                    this._hotSheetservice.deletePurchaseOrder(e.row.data.purchaseOrderId)
                    .pipe(
                        finalize(() => {                                    
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SuccessfullyDeleted'), this.l('Purchase Order'));                            
                        this.refresh();
                    });
                }
            });           
       
          };

        public onSavingPurchaseOrder(e: any): void {
                const change = e.changes[0];       

                if (change || this.Editing) {
                    e.cancel = true;
        
                    if (change != undefined && change.type === 'remove') {    

                        //LHH: Esto ya no funciona ya que pusimos [confirmDelete]="false" y nos fuimos por un boton personalizado
                        //cambiando: <!-- <dxi-button name="delete"></dxi-button>     -->
                        //por : <dxi-button icon="trash" hint="Eliminar" [onClick]="deletePurchaseOrder"></dxi-button>                            
                        //por lo cual lo comentamos, jala bien pero trae un confirm por default sn permitir personalizar el texto.
                          
                        // this._hotSheetservice.deletePurchaseOrder(change.key)
                        // .pipe(
                        //     finalize(() => {                                    
                        //     })
                        // )
                        // .subscribe(() => {
                        //     this.notify.success(this.l('SuccessfullyDeleted'), this.l('Purchase Order'));                            
                        //     this.refresh();
                        // });
                
                    } else if ((change != undefined && change.type === 'insert') || this.Editing) {
                        this._hotSheetservice
                            .createOrUpdatePurchaseOrder(this.purchaseOrdersChanges)
                            .pipe(
                                finalize(() => {                                    
                                })
                            )
                            .subscribe(() => {
                                this.notify.success(this.l('SavedSuccessfully'), this.l('Purchase Order'));
                                this.refresh();
                            });
                    }
        
                    e.changes = [];
                    e.component.cancelEditData();
                    this.Editing = false;
                }
        
                console.log('onSavingHotSheet', e, this.purchaseOrdersChanges);
            }

    public refresh(): void {
        this.isTableLoading = true;
        let statusOpen = 0;
      

        let input: GetHotSheetInput = new GetHotSheetInput();
        input.startDate = this.startDate ? moment(this.startDate) : undefined;
        input.endDate = this.endDate ? moment(this.endDate) : undefined;
        input.statusHS = statusOpen;
        
        this._hotSheetservice
            .getPurchaseOrders(input)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: PurchaseOrdersItemDto[]) => {
                this.purchaseOrders = response;
               
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
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'purchaseOrdersReport_' + Date.now().toString() + '.xlsx');
                });
            });
            e.cancel = true;
        } else if (e.format === 'pdf') {
            const doc = new jsPDF();
            exportDataGridToPdf({
                jsPDFDocument: doc,
                component: e.component,
            }).then(() => {
                doc.save('purchaseOrdersReport_' + Date.now().toString() + '.pdf');
            });
        }
    }
  

    public get enabledEditInfo(): boolean {
        return (
            this.appSession.user.isPC || this.appSession.user.isPC            
        );
    }


    private coloresPorFecha = new Map<string, string>();

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


}
