import { Component, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CatalogServiceProxy, UserServiceProxy, StaffDto, UserDto, UserDtoPagedResultDto, UserByCurrentUserDto } from '@shared/service-proxies/service-proxies';
import { cloneDeep } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';
import { BsModalService } from 'ngx-bootstrap/modal';
import notify from 'devextreme/ui/notify';
import { environment } from 'environments/environment';
import DataSource from 'devextreme/data/data_source';

// https://js.devexpress.com/Demos/WidgetsGallery/Demo/DataGrid/RowEditingAndEditingEvents/Angular/Light/

@Component({
    selector: 'app-staff',
    templateUrl: './staff.component.html',
    styleUrls: ['./staff.component.css'],
    animations: [appModuleAnimation()],
})
export class StaffComponent extends AppComponentBase implements OnInit {
    @Input() staff: StaffDto[] = [];
    @Output() onstaffChanged = new EventEmitter<StaffDto[]>();

    //users: UserDto[] = [];
    //users: UserByCurrentUserDto[] = [];
    staffChanges: StaffDto = new StaffDto();
    usersDataSource: DataSource;

    isTableLoading: boolean = false;
    isLoadingData: boolean = false;
    editing: boolean = false;
    keyword = '';
    staffItems: string[];
    optionType: string = 'IE Staff';

    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;

    constructor(injector: Injector, private _catalogService: CatalogServiceProxy, private _userService: UserServiceProxy) {
        super(injector);

        this.staffItems = ['IE Staff', 'Accounting Staff'];
    }

    onValueChanged($event) {
        let valor = $event.value;
        valor == 'IE Staff' ? (this.staffChanges.type = 1) : (this.staffChanges.type = 2);
    }

    public ngOnInit(): void {
        this.refresh();

        staffItems: [
            { id: 1, name: 'IE Staff' },
            { id: 2, name: 'Accounting Staff' },
        ];

        this.isLoadingData = true;

        let pageSize = 100000;
        let pageNumber = 1;

        let maxResultCount = pageSize;
        let skipCount = (pageNumber - 1) * pageSize;
        Promise.all([
            this._userService
                .getAll(this.keyword, true, skipCount, maxResultCount)
                .pipe(
                    finalize(() => {
                        this.isLoadingData = false;
                    })
                )
                .subscribe((result: UserDtoPagedResultDto) => {
                    this.usersDataSource = new DataSource({
                        store: result.items,
                        paginate: true,
                        pageSize: 50,
                    });
                }),
        ]);

        // Promise.all([
        //     this._userService.getUsersByCurrentUser().toPromise()
        // ]).then((responses) => {
        //     this.users = responses[0];
        //     this.isLoadingData = false;
        // });
    }

    public onInitNewRow(event: any): void {
        this.staffChanges = new StaffDto();
        this.staffChanges.type = 1;
        this.optionType = 'IE Staff';
    }

    public onEditingStart(event: any): void {
        this.staffChanges = new StaffDto();
        let newStaff = new StaffDto();
        newStaff = this.staff.find((pn: StaffDto) => pn.id === event.data.id);
        this.staffChanges.id = event.data.id;
        this.staffChanges.type = newStaff.type;

        newStaff.type == 1 ? (this.optionType = 'IE Staff') : (this.optionType = 'Accounting Staff');
        this.staffChanges.userId = newStaff.userId;
        this.staffChanges.isActive = newStaff.isActive;
        this.editing = true;
    }

    public refresh(): void {
        this.isTableLoading = true;
        this._catalogService
            .getStaff(undefined)
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                })
            )
            .subscribe((response: StaffDto[]) => {
                this.staff = response;
            });
    }

    public onEditCanceling(event: any) {
        this.editing = false;
    }

    public onSavingStaff(e: any): void {
        const change = e.changes[0];

        if (change || this.editing) {
            e.cancel = true;

            if (change != undefined && change.type === 'remove') {
                // this._catalogService.delete(item.id).subscribe(() => {
                //   abp.notify.success(this.l('SuccessfullyDeleted'));
                //   this.refresh();
                // });
                abp.notify.success(this.l('SuccessfullyDeleted'));
            } else if ((change != undefined && change.type === 'insert') || this.editing) {
                this._catalogService
                    .createOrUpdateStaff(this.staffChanges)
                    .pipe(
                        finalize(() => {
                            // this.saving = false;
                            // this.saveLabel = this.l('Save');
                        })
                    )
                    .subscribe(() => {
                        this.notify.success(this.l('SavedSuccessfully'));
                        this.refresh();
                    });
            }

            e.changes = [];
            e.component.cancelEditData();
            this.editing = false;
        }

        //console.log('onSavingStaff', e, this.staffChanges);
    }
}
