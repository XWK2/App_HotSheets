import { Component, Injector, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { UserServiceProxy, UserDto, RoleDto, PlantDto, DepartmentDto, CatalogServiceProxy, EmployeeDto } from '@shared/service-proxies/service-proxies';
import DataSource from 'devextreme/data/data_source';

@Component({
    templateUrl: './edit-user-dialog.component.html',
})
export class EditUserDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    user = new UserDto();
    roles: RoleDto[] = [];
    checkedRolesMap: { [key: string]: boolean } = {};
    id: number;

    departments: DepartmentDto[] = [];
    plants: PlantDto[] = [];
    employees: EmployeeDto[] = [];
    employeeDataSource: DataSource;
    isDataLoading: boolean = false;
    @Output() onSave = new EventEmitter<any>();

    constructor(injector: Injector, public _userService: UserServiceProxy, public bsModalRef: BsModalRef, private _catalogService: CatalogServiceProxy) {
        super(injector);
    }

    ngOnInit(): void {
        this.isDataLoading = true;
        Promise.all([
            this._userService.get(this.id).toPromise(),
            this._userService.getRoles().toPromise(),
            this._catalogService.getDepartments(true).toPromise(),
            this._catalogService.getPlants(true).toPromise(),
            this._catalogService.getEmployeesList().toPromise(),
        ]).then((responses) => {
            this.user = responses[0];

            this.roles = responses[1].items;
            this.setInitialRolesStatus();

            this.departments = responses[2];
            this.plants = responses[3];

            this.employees = responses[4];
            this.employeeDataSource = new DataSource({
                store: this.employees,
                paginate: true,
                pageSize: 50,
            });

            this.isDataLoading = false;
        });
    }

    displayExpression(data: EmployeeDto): string {
        return data ? data.densoEmployeeId + ' - ' + data.name + ' ' + data.surnames : null;
    }

    setInitialRolesStatus(): void {
        _map(this.roles, (item) => {
            this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(item.normalizedName);
        });
    }

    isRoleChecked(normalizedName: string): boolean {
        return _includes(this.user.roleNames, normalizedName);
    }

    onEmployeeChange($event) {
        let employeId: number = $event.value;
        if (employeId != null) {
            let employeeSelected: EmployeeDto = this.employees.find((e) => e.id == employeId);
            if (employeeSelected) {
                // this.user.userName = employeeSelected.densoEmployeeId?.toString();
                this.user.name = employeeSelected.name;
                this.user.surname = employeeSelected.surnames;
                this.user.emailAddress = employeeSelected.emailAddress;
                this.user.employeeId = employeeSelected.id;
            }
        }
    }

    onRoleChange(role: RoleDto, $event) {
        this.checkedRolesMap[role.normalizedName] = $event.target.checked;
    }

    getCheckedRoles(): string[] {
        const roles: string[] = [];
        _forEach(this.checkedRolesMap, function (value, key) {
            if (value) {
                roles.push(key);
            }
        });
        return roles;
    }

    save(): void {
        this.saving = true;

        this.user.roleNames = this.getCheckedRoles();

        this._userService.update(this.user).subscribe(
            () => {
                this.notify.info(this.l('SavedSuccessfully'), 'Admin');
                this.bsModalRef.hide();
                this.onSave.emit();
            },
            () => {
                this.saving = false;
            }
        );
    }
}
