import { Component, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, HotSheetReasonDto } from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'hot-sheet-reasons',
    templateUrl: './hot-sheet-reasons.component.html',
    styleUrls: ['./hot-sheet-reasons.component.css'],
    animations: [appModuleAnimation()],
})
export class HotSheetReasonsComponent extends AppComponentBase implements OnInit {
    @Input() hotSheetReasons: HotSheetReasonDto[] = [];
    @Output() onShipmentReasonChanged = new EventEmitter<HotSheetReasonDto[]>();

    //hotSheetReasons: HotSheetReasonDto[] = [];
    isTableLoading: boolean = false;
    Editing: boolean = false;

    hotSheetReasonsChanges: HotSheetReasonDto = new HotSheetReasonDto();
    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);
    }

    public ngOnInit(): void {
        this.refresh();
    }

    public onInitNewRow(event: any): void {
        this.hotSheetReasonsChanges = new HotSheetReasonDto();
    }

    public onEditingStart(event: any): void {
        this.hotSheetReasonsChanges = new HotSheetReasonDto();
        let newhotSheetReason = new HotSheetReasonDto(); //cloneDeep(this.unitMeasureSatChanges);
        newhotSheetReason = this.hotSheetReasons.find((pn: HotSheetReasonDto) => pn.id === event.data.id);
        this.hotSheetReasonsChanges.id = event.data.id;
        this.hotSheetReasonsChanges.description = newhotSheetReason.description;
        this.hotSheetReasonsChanges.bNoticeRMARequired = newhotSheetReason.bNoticeRMARequired;
        this.hotSheetReasonsChanges.pictureTechnicalInfoMakerModelSerialNumber = newhotSheetReason.pictureTechnicalInfoMakerModelSerialNumber;
        this.hotSheetReasonsChanges.attachPurchaseOrder = newhotSheetReason.attachPurchaseOrder;
        this.hotSheetReasonsChanges.technicalInfoPicture = newhotSheetReason.technicalInfoPicture;
        this.hotSheetReasonsChanges.accountingApprovalRequired = newhotSheetReason.accountingApprovalRequired;
        this.hotSheetReasonsChanges.excludeTermOfPayment = newhotSheetReason.excludeTermOfPayment;
        this.hotSheetReasonsChanges.remittence = newhotSheetReason.remittence;
        this.hotSheetReasonsChanges.noPayment = newhotSheetReason.noPayment;
        this.hotSheetReasonsChanges.isActive = newhotSheetReason.isActive;
        this.Editing = true;
    }

    public onEditCanceling(event: any) {
        this.Editing = false;
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getHotSheetReasons(undefined, undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: HotSheetReasonDto[]) => {
                this.hotSheetReasons = response;
            });
    }

    public onSavingHotSheetReason(e: any): void {
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
                    .createOrUpdateHotSheetReason(this.hotSheetReasonsChanges)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'), this.l('hotSheetReasons'));
                        this.refresh();
                    });
            }

            e.changes = [];
            e.component.cancelEditData();
            this.Editing = false;
        }
    }

    // public onSave(event: any): void {
    //     if (event.changes && event.changes.length) {
    //         let item = event.changes[0];
    //         if (item.type && item.data) {
    //             let itemToCreateOrUpdate = this.hotSheetReasons.find((p) => p.id == item.key);
    //             if (itemToCreateOrUpdate && item.type === 'update') {
    //                 for (const [key, value] of Object.entries(item.data)) {
    //                     itemToCreateOrUpdate[key] = value;
    //                 }
    //             } else if (item.type === 'insert') {
    //                 itemToCreateOrUpdate = cloneDeep(item.data);
    //                 itemToCreateOrUpdate.id = null;
    //             }

    //             this._catalogService
    //                 .createOrUpdateShipmentReason(itemToCreateOrUpdate)
    //                 .pipe(
    //                     finalize(() => {
    //                         // this.saving = false;
    //                         // this.saveLabel = this.l('Save');
    //                     })
    //                 )
    //                 .subscribe(() => {
    //                     this.notify.info(this.l('SavedSuccessfully'));
    //                     this.refresh();
    //                 });
    //         }
    //     }
    // }
}
