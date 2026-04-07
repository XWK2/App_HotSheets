import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { HotSheetServiceProxy, HotSheetsCommetsDto } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-hotSheet-comments-popup',
    templateUrl: './comments-popup.component.html',
    styleUrls: ['./comments-popup.component.css'],
    animations: [appModuleAnimation()],
})
export class CommentsPopupComponent extends AppComponentBase implements OnInit {
    @Input() comment: HotSheetsCommetsDto;
    @Input() showPopup: boolean = false;
    @Output() onClose = new EventEmitter();

    constructor(injector: Injector,private _hotSheetservice: HotSheetServiceProxy) {
        super(injector);       
        this.onClosePopup = this.onClosePopup.bind(this);
    }

    public ngOnInit(): void {
        
    }

    public onClosePopup(e: any): void {
        this.onClose.emit();
    }

    public onSavingHotSheetComments(sender: any): void {
        
        //const valueComments = this.comment.comments;  
        //aqui podemos guardar el comentario.
        
        this.comment.creatorUserId = 1;
        this.comment.departmentId = 1;

        if(this.comment.comments != "")
        {
            this._hotSheetservice
            .createOrUpdateHotSheetComments(this.comment)        
            .subscribe(() => {
                this.notify.success(this.l('SavedSuccessfully'), this.l('Hot Sheet Comments'));
                //this.refresh();
                this.onClose.emit();
            },
            () => {
                //this.showCommentsPopup = true;
                //this.saving = false;
            });
        
        }
    }
}
