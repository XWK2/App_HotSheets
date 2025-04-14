import { Component, EventEmitter, Injector, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { UnitMeasureDto } from '@shared/service-proxies/service-proxies';

// Missing make it work Required property
// https://plnkr.co/edit/f6GsLE1djtAYbHMgArnr?p=preview&preview

@Component({
    selector: 'denso-unit-measures-select',
    templateUrl: './unit-measures-select.component.html',
    styleUrls: ['./unit-measures-select.component.css'],
})
export class UnitMeasuresSelectComponent extends AppComponentBase implements OnInit {
    @Input() items: UnitMeasureDto[] = [];
    @Input() value: number;
    @Output() onValueChanged = new EventEmitter<number>();

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        console.log('value', this.value);
    }

    ngOnChanges(changes: SimpleChanges) {
        console.log('ngOnChanges', changes);
    }

    public onUnitMeasureChanged(e: any): void {
        if (e) {
            this.onValueChanged.emit(e.value);
        }
    }

    public getUnitMeasureItemText(item: UnitMeasureDto): string {
        if (item) {
            return (
                item.name +
                ' - ' +
                (item.densoCode ? 'Denso: ' + item.densoCode : '') +
                (item.satCode ? ', Sat: ' + item.satCode : '') +
                (item.segroveCode ? ', Segrove: ' + item.segroveCode : '')
            );
        } else {
            return null;
        }
    }
}
