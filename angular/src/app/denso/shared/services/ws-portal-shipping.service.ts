import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class WsPortalShippingService {
    public UpdateShippingInfo(wsUrls: string[], catalogName: string, component: any): void {
        let requestOptions = {
            method: 'POST',
            redirect: 'follow',
        } as any;

        abp.message.confirm(component.l('AreYouSureWantToUpdateShippingInfo', catalogName), component.l('AreYouSure'), async (result: boolean) => {
            if (result) {
                abp.ui.setBusy();
                await Promise.all(wsUrls.map((u) => fetch(u, requestOptions)))
                    .then((responses) => {
                        responses.map((response) => {
                            console.log(response.status);
                            component.notify.success(component.l('UpdateShippingInfoUpdated', catalogName), component.l('HotSheetInfo'));
                            abp.ui.clearBusy();
                        });
                    })
                    .catch((error) => console.log('error', error));
            }
        });
    }
}
