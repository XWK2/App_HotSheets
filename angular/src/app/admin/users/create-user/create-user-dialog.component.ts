import { Component, Injector, OnInit, EventEmitter, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { UserServiceProxy, CreateUserDto, RoleDto, DepartmentDto, PlantDto, CatalogServiceProxy, EmployeeDto } from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';
import DataSource from 'devextreme/data/data_source';

@Component({
    templateUrl: './create-user-dialog.component.html',
})
export class CreateUserDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    user = new CreateUserDto();
    roles: RoleDto[] = [];
    checkedRolesMap: { [key: string]: boolean } = {};
    defaultRoleCheckedStatus = false;
    passwordValidationErrors: Partial<AbpValidationError>[] = [
        {
            name: 'pattern',
            localizationKey: 'PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber',
        },
    ];
    confirmPasswordValidationErrors: Partial<AbpValidationError>[] = [
        {
            name: 'validateEqual',
            localizationKey: 'PasswordsDoNotMatch',
        },
    ];

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
        this.user.isActive = true;

        this.isDataLoading = true;
        Promise.all([
            this._userService.getRoles().toPromise(),
            this._catalogService.getDepartments(true).toPromise(),
            this._catalogService.getPlants(true).toPromise(),
            this._catalogService.getEmployeesList().toPromise(),
        ]).then((responses) => {
            this.roles = responses[0].items;
            this.setInitialRolesStatus();

            this.departments = responses[1];
            this.plants = responses[2];

            this.employees = responses[3];
            this.employeeDataSource = new DataSource({
                store: this.employees,
                paginate: true,
                pageSize: 50,
            });

            this.isDataLoading = false;
        });
    }

    setInitialRolesStatus(): void {
        _map(this.roles, (item) => {
            this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(item.normalizedName);
        });
    }

    isRoleChecked(normalizedName: string): boolean {
        // just return default role checked status
        // it's better to use a setting
        return this.defaultRoleCheckedStatus;
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

        this._userService.create(this.user).subscribe(
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

    onEmployeeChange($event) {
        let employeeId: number = $event.value;
        if (employeeId != null) {
            let employeeSelected = this.employees.find((e) => e.id == employeeId);
            if (employeeSelected) {
                this.user.userName = employeeSelected.densoEmployeeId?.toString();
                this.user.name = employeeSelected.name;
                this.user.surname = employeeSelected.surnames;
                this.user.emailAddress = employeeSelected.emailAddress;
                this.user.employeeId = employeeSelected.id;
            }
        }
    }
}
