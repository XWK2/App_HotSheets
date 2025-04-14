import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { FileDto } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-hotSheet-document-viewer-popup',
    templateUrl: './document-viewer-popup.component.html',
    styleUrls: ['./document-viewer-popup.component.css'],
    animations: [appModuleAnimation()],
})
export class DocumentViewerPopupComponent extends AppComponentBase implements OnInit {
    @Input() document: FileDto;
    @Input() showPopup: boolean = false;
    @Output() onClose = new EventEmitter();

    constructor(injector: Injector) {
        super(injector);

        this.onClosePopup = this.onClosePopup.bind(this);
    }

    public ngOnInit(): void {}

    public onClosePopup(e: any): void {
        this.onClose.emit();
    }
}
