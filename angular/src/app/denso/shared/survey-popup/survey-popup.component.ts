import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { SurveyDto, SurveyServiceProxy } from '@shared/service-proxies/service-proxies';
import { DxPopupComponent } from 'devextreme-angular';

@Component({
    selector: 'app-survey-popup',
    templateUrl: './survey-popup.component.html',
    styleUrls: ['./survey-popup.component.css'],
})
export class SurveyPopupComponent extends AppComponentBase implements OnInit {
    @Input() pageName: string;
    showSurveyPopup: boolean = false;
    surveyAnswers: SurveyDto = new SurveyDto();
    @ViewChild(DxPopupComponent, { static: false }) popup: DxPopupComponent;

    constructor(injector: Injector, private _surveyService: SurveyServiceProxy) {
        super(injector);

        this.onPopupClose = this.onPopupClose.bind(this);
        this.onSurveyFormSubmit = this.onSurveyFormSubmit.bind(this);
    }

    ngOnInit(): void {}

    public showSurvey(): void {
        this.showSurveyPopup = true;
        this.surveyAnswers = new SurveyDto();
        this.surveyAnswers.page = this.pageName;
        this.surveyAnswers.answerQuestion1 = '1';
        this.surveyAnswers.answerQuestion2 = null;
        this.surveyAnswers.answerQuestion3 = null;
        //this.surveyAnswers.shippingInstruction = null;
    }

    public onPopupClose(): void {
        this.showSurveyPopup = false;
        if (this.popup && this.popup.instance) {
            this.popup.instance.hide();
        }
    }

    public onSelectAnswer1(value: any): void {
        this.surveyAnswers.answerQuestion1 = value;
    }

    public onSurveyFormSubmit(e: any): void {
        abp.message.confirm(this.l('AreYouSureWantToSendSurvey'), this.l('AreYouSure'), (result: boolean) => {
            if (result) {
                this._surveyService.saveSurvey(this.surveyAnswers).subscribe(() => {
                    this.notify.success(this.l('SentSuccessfully'), this.l('Surveys'));
                    this.onPopupClose();
                });
            }
        });
    }
}
