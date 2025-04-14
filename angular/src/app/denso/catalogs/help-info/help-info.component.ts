import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, HelpInfoDto, HelpInfoFieldDto } from '@shared/service-proxies/service-proxies';
import { DxDataGridComponent } from 'devextreme-angular';
import { finalize } from 'rxjs/operators';

// https://js.devexpress.com/Demos/WidgetsGallery/Demo/HtmlEditor/Overview/Angular/Light/

@Component({
    selector: 'app-help-info',
    templateUrl: './help-info.component.html',
    styleUrls: ['./help-info.component.css'],
    animations: [appModuleAnimation()],
})
export class HelpInfoComponent extends AppComponentBase implements OnInit {
    helpInfoItems: HelpInfoDto[] = [];
    helpInfoFields: HelpInfoFieldDto[] = [];
    isTableLoading: boolean = false;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    helpInfoChanges: HelpInfoDto = new HelpInfoDto();

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);

        this.onClosePopup = this.onClosePopup.bind(this);
        this.onSaveChanges = this.onSaveChanges.bind(this);
    }

    public ngOnInit(): void {
        this._catalogService
            .getHelpInfoFields()
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: HelpInfoFieldDto[]) => {
                this.helpInfoFields = response;
            });

        this.refresh();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getHelpInfo()
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: HelpInfoDto[]) => {
                this.helpInfoItems = response;
            });
    }

    public onInitNewRow(event: any): void {
        this.helpInfoChanges = new HelpInfoDto();
    }

    public onEditingStart(e: any): void {
        this.helpInfoChanges = new HelpInfoDto();
        let newHelpInfo = new HelpInfoDto(); //cloneDeep(this.unitMeasureSatChanges);
        newHelpInfo = this.helpInfoItems.find((pn: HelpInfoDto) => pn.id === e.data.id);
        this.helpInfoChanges.id = e.data.id;
        this.helpInfoChanges.helpInfoFieldId = newHelpInfo.helpInfoFieldId;
        this.helpInfoChanges.helpTextEnglish = newHelpInfo.helpTextEnglish;
        this.helpInfoChanges.helpTextSpanish = newHelpInfo.helpTextSpanish;
        this.helpInfoChanges.isActive = newHelpInfo.isActive;
    }

    public onSave(e: any): void {
        this._catalogService
            .createOrUpdateHelpInfo(this.helpInfoChanges)
            .pipe(
                finalize(() => {
                    // this.saving = false;
                    // this.saveLabel = this.l('Save');
                })
            )
            .subscribe(() => {
                this.notify.success(this.l('SavedSuccessfully'), this.l('Departments'));
                this.refresh();
            });
        //console.log(change, this.helpInfoChanges);
        //debugger;
        // if (e.changes && e.changes.length) {
        //     e.cancel = true;
        //     let item = e.changes[0];
        //     if (item.type && item.data) {
        //         let itemToCreateOrUpdate = this.helpInfoItems.find((p) => p.id == item.key);
        //         if (itemToCreateOrUpdate && item.type === 'update') {
        //             for (const [key, value] of Object.entries(item.data)) {
        //                 itemToCreateOrUpdate[key] = value;
        //             }
        //         } else if (item.type === 'insert') {
        //             itemToCreateOrUpdate = cloneDeep(item.data);
        //             itemToCreateOrUpdate.id = null;
        //         }
        //         this._catalogService
        //             .createOrUpdateHelpInfo(itemToCreateOrUpdate)
        //             .pipe(
        //                 finalize(() => {
        //                     // this.saving = false;
        //                     // this.saveLabel = this.l('Save');
        //                 })
        //             )
        //             .subscribe(() => {
        //                 this.notify.success(this.l('SavedSuccessfully'), this.l('Departments'));
        //                 this.refresh();
        //             });
        //     }

        //     e.changes = [];
        //     e.component.cancelEditData();
        // }
    }

    public onClosePopup(e: any): void {
        this.dataGrid.instance.cancelEditData();
    }

    public onSaveChanges(e: any): void {
        this.dataGrid.instance.saveEditData();
    }
}
