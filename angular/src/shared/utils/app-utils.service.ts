import { Injectable } from '@angular/core';
import { formatDate } from '@angular/common';
//import { HelpInfoDto, HotSheetHistoryDto } from '@shared/service-proxies/service-proxies';
import { HelpInfoDto } from '@shared/service-proxies/service-proxies';

@Injectable()
export class AppUtilsService {
    public helpInfoData: HelpInfoDto[] = [];
    public targetHelpPopover: string;
    public helpTextPopover: string;

    public formatDate(data: any, locale: string, format: string = 'YYYY-MM-dd HH:mm'): any {
        return formatDate(data.toDate(), format, locale);
    }

    public setHelpInfo(data: HelpInfoDto[]): void {
        this.helpInfoData = data;
    }

    public loadHelpInfoPopovers(currentLanguage: abp.localization.ILanguageInfo, miliseconds: number = 1000): void {
        setTimeout(() => {
            let helpInfoFieldValues: string[] = this.helpInfoData
                .filter(
                    (dataItem) =>
                        dataItem.isActive === true &&
                        ((dataItem.helpTextEnglish && currentLanguage.name === 'en') || (dataItem.helpTextSpanish && currentLanguage.name !== 'en'))
                )
                .map((helpItem) => helpItem.helpInfoField.fieldName);

            let helpInfoFields = document.querySelectorAll('[help-info-field]');

            helpInfoFields.forEach((helpInfoFieldElem: Element) => {
                let helpInfoFieldValue = helpInfoFieldElem.getAttribute('help-info-field');
                if (helpInfoFieldValue && helpInfoFieldValues.includes(helpInfoFieldValue)) {
                    let parentElem = helpInfoFieldElem.closest('div.dx-field-item');
                    let fieldItemLabels = parentElem.getElementsByClassName('dx-field-item-label-text');

                    if (fieldItemLabels.length > 0) {
                        let targetFieldItemLabel = fieldItemLabels[0];

                        // Set target elements for popover
                        targetFieldItemLabel.setAttribute('help-info-target', helpInfoFieldValue);

                        targetFieldItemLabel.addEventListener('mouseover', (event: any) => {
                            let targetHelpInfoFieldValue = targetFieldItemLabel.getAttribute('help-info-target');
                            let helpInfoDataTarget = this.helpInfoData.find(
                                (helpInfoItem) => helpInfoItem.helpInfoField.fieldName === targetHelpInfoFieldValue
                            );

                            if (targetHelpInfoFieldValue && helpInfoDataTarget) {
                                this.targetHelpPopover = `[help-info-target='${targetHelpInfoFieldValue}']`;
                                if (currentLanguage.name === 'en') {
                                    this.helpTextPopover = helpInfoDataTarget.helpTextEnglish;
                                } else {
                                    this.helpTextPopover = helpInfoDataTarget.helpTextSpanish;
                                }
                            } else {
                                this.targetHelpPopover = `[help-info-target='NotFound']`;
                            }
                        });
                    }
                }
            });
        }, miliseconds);
    }

    /* public getLogsText(history: HotSheetHistoryDto[], parentContext: any, nextLine: string = '\n'): string {
        let logs: string[] = [];
        history.forEach((h) => {
            const dateFormat: string = this.formatDate(h.creationTime, parentContext.locale);
            if (h.historyType === 'ApprovalRequested') {
                logs.push(`${dateFormat} - ${parentContext.l('SentForApprovalBy')} ${h.creatorUserName}`);
            } else if (h.historyType === 'EmailNotificationSent') {
                logs.push(`${dateFormat} - ${parentContext.l('EmailNotificationSent')} ${h.creatorUserName ?? ''}`);
            } else if (h.historyType === 'Approved') {
                logs.push(`${dateFormat} - ${parentContext.l('ApprovedBy')} ${h.creatorUserName} - ${h.comments}`);
            } else if (h.historyType === 'Rejected') {
                logs.push(`${dateFormat} - ${parentContext.l('RejectedBy')} ${h.creatorUserName} - ${h.comments}`);
            } else if (h.historyType === 'ApproverChanged') {
                logs.push(`${dateFormat} - ${parentContext.l('ApproverChangedBy')} ${h.creatorUserName} - ${h.comments}`);
            } else if (h.historyType === 'Reseted') {
                logs.push(`${dateFormat} - ${parentContext.l('ResetedBy')} ${h.creatorUserName} - ${h.comments ?? ''}`);
            } else if (h.historyType === 'ExportedToAS400') {
                logs.push(`${dateFormat} - ${parentContext.l('ExportedToAS400')} ${h.comments ?? ''}`);
            } else {
                logs.push(`${dateFormat} - ${parentContext.l('UpdatedBy')} ${h.creatorUserName} - ${h.comments ?? ''}`);
            }
        });

        return logs.join(nextLine);
    } */

    public getPopupHeightByScreen(screenHeight: number): number {
        let popupHeight: number = 850;
        if (screenHeight > 900) {
            popupHeight = 850;
        } else if (screenHeight > 800) {
            popupHeight = 750;
        } else if (screenHeight > 700) {
            popupHeight = 650;
        } else if (screenHeight > 600) {
            popupHeight = 550;
        } else {
            popupHeight = 850;
        }
        return popupHeight;
    }
}
