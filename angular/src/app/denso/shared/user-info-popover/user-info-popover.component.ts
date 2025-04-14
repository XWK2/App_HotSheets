import { Component, Injector, Input, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { UserLoginInfoDto } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-user-info-popover',
    templateUrl: './user-info-popover.component.html',
    styleUrls: ['./user-info-popover.component.css'],
})
export class UserInfoPopoverComponent extends AppComponentBase implements OnInit {
    @Input() user: UserLoginInfoDto;
    @Input() type: string;
    @Input() creationDate: Date;

    public userDepartmentNames: string;
    public userPlantNames: string;

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        if (this.user) {
            this.userDepartmentNames = this.user.departments.map((d) => d.name).join(', ');
            this.userPlantNames = this.user.plants.map((d) => d.name).join(', ');
        } else {
            this.user = {} as UserLoginInfoDto;
        }
        if (!this.creationDate) {
            this.creationDate = new Date();
        }
    }
}
