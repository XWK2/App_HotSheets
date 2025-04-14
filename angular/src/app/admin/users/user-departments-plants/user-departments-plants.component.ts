import { Component, Injector, OnInit, EventEmitter, Output, Input, ChangeDetectionStrategy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
    UserServiceProxy,
    UserDto,
    CatalogServiceProxy,
    DepartmentDto,
    PlantDto,
    DepartmentUserDto,
    PlantUserDto,
    CreateUserDto,
} from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-user-departments-plants',
    templateUrl: './user-departments-plants.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserDepartmentsPlantsComponent extends AppComponentBase implements OnInit {
    @Input() user: UserDto | CreateUserDto;
    @Input() departments: DepartmentDto[] = [];
    @Input() plants: PlantDto[] = [];

    departmentIds: number[];

    @Output() onDeptosChanged = new EventEmitter<any>();
    @Output() onPlantsChanged = new EventEmitter<any>();

    constructor(
        injector: Injector,
        public _userService: UserServiceProxy,
        public bsModalRef: BsModalRef,
        private _catalogService: CatalogServiceProxy
    ) {
        super(injector);
    }

    public ngOnInit(): void {}

    public getDeptoName(id: number): string {
        return this.departments.find((item) => item.id == id)?.name;
    }

    public onDeptoChange(deptos: DepartmentDto[]): void {
        if (!this.user.departments) {
            this.user.departments = [];
        }

        deptos.forEach((deptoItem) => {
            let deptoExists = this.user.departments.find((item) => item.departmentId == deptoItem.id);
            if (!deptoExists) {
                let deptoUser: DepartmentUserDto = new DepartmentUserDto();
                deptoUser.departmentId = deptoItem.id;
                deptoUser.isSupervisor = false;
                this.user.departments.push(deptoUser);
            }
        });

        this.user.departments.forEach((deptoItem, index) => {
            let deptoExists = deptos.find((item) => item.id == deptoItem.departmentId);
            if (!deptoExists) {
                this.user.departments.splice(index, 1);
            }
        });
    }

    public getPlantName(id: number): string {
        return this.plants.find((item) => item.id == id)?.name;
    }

    public onPlantChange(plants: PlantDto[]): void {
        if (!this.user.plants) {
            this.user.plants = [];
        }

        plants.forEach((plantItem) => {
            let teamExists = this.user.plants.find((item) => item.plantId == plantItem.id);
            if (!teamExists) {
                let userPlant: PlantUserDto = new PlantUserDto();
                userPlant.plantId = plantItem.id;
                userPlant.isSupervisor = false;
                this.user.plants.push(userPlant);
            }
        });

        this.user.plants.forEach((plantItem, index) => {
            let plantExists = plants.find((item) => item.id == plantItem.plantId);
            if (!plantExists) {
                this.user.plants.splice(index, 1);
            }
        });
    }

    public onDeptoSupervisorChange(item: DepartmentUserDto, event: any): void {
        item.isSupervisor = event.target.checked;
    }

    public onPlantSupervisorChange(item: PlantUserDto, event: any): void {
        item.isSupervisor = event.target.checked;
    }
}
