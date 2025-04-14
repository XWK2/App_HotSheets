import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { DxDrawerComponent } from 'devextreme-angular';
import { DrawerProps } from '../models/drawer.props';

@Component({
    selector: 'app-address-details-drawer',
    templateUrl: './address-details-drawer.component.html',
    styleUrls: ['./address-details-drawer.component.css'],
})
export class AddressDetailsDrawerComponent extends AppComponentBase implements OnInit {
    @ViewChild(DxDrawerComponent, { static: false }) drawer: DxDrawerComponent;

    drawerConfig: any = {
        openMode: 'shrink',
        position: 'right',
        revealMode: 'slide',
    };

    @Input() drawerProps: DrawerProps = new DrawerProps();

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {}

    public onClose(): void {
        this.drawerProps.isDrawerOpen = false;
    }
}
