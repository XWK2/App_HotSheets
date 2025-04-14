import { DensoDocumentStatus } from '@shared/AppEnums';
import { ShippingInstructionApprovalType } from '@shared/service-proxies/service-proxies';

export class PopupApproval {
    show: boolean;
    title: string;
    type: ShippingInstructionApprovalType;
    comments: string;
    msgConfirm: string;
    status: DensoDocumentStatus;
}
