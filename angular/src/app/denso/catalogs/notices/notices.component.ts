import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, NoticeDto } from '@shared/service-proxies/service-proxies';
import { DxDataGridComponent } from 'devextreme-angular';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-notices',
    templateUrl: './notices.component.html',
    styleUrls: ['./notices.component.css'],
    animations: [appModuleAnimation()],
})
export class NoticesComponent extends AppComponentBase implements OnInit {
    notices: NoticeDto[] = [];
    isTableLoading: boolean = false;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    noticeChanges: NoticeDto = new NoticeDto();

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy) {
        super(injector);

        this.onClosePopup = this.onClosePopup.bind(this);
        this.onSaveChanges = this.onSaveChanges.bind(this);
    }

    public ngOnInit(): void {
        this.refresh();
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getNotices()
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: NoticeDto[]) => {
                this.notices = response;
            });
    }

    public onInitNewRow(event: any): void {
        this.noticeChanges = new NoticeDto();
    }

    public onEditingStart(e: any): void {
        this.noticeChanges = new NoticeDto();
        let noticeInfo = this.notices.find((pn: NoticeDto) => pn.id === e.data.id);
        this.noticeChanges.id = e.data.id;
        this.noticeChanges.message = noticeInfo.message;
        this.noticeChanges.noticeDay = noticeInfo.noticeDay;
        this.noticeChanges.anticipationDays = noticeInfo.anticipationDays;
        this.noticeChanges.isActive = noticeInfo.isActive;
    }

    public onSave(e: any): void {
        this._catalogService
            .createOrUpdateNotice(this.noticeChanges)
            .pipe(
                finalize(() => {
                    // this.saving = false;
                    // this.saveLabel = this.l('Save');
                })
            )
            .subscribe(() => {
                this.notify.success(this.l('SavedSuccessfully'), this.l('Notices'));
                this.refresh();
            });
    }

    public onClosePopup(e: any): void {
        this.dataGrid.instance.cancelEditData();
    }

    public onSaveChanges(e: any): void {
        this.dataGrid.instance.saveEditData();
    }
}
