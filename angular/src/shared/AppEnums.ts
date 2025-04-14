import { TenantAvailabilityState } from '@shared/service-proxies/service-proxies';

export class AppTenantAvailabilityState {
    static Available: number = TenantAvailabilityState._1;
    static InActive: number = TenantAvailabilityState._2;
    static NotFound: number = TenantAvailabilityState._3;
}

export class DensoDocumentStatus {
    static Draft: number = 1;
    static Circulating: number = 2;
    static Approved: number = 3;
    static Cancelled: number = 4;
    static PendingForApproval: number = 5;
    static Rejected: number = 6;
}

export enum ShippingPageType {
    ViewShippingDetails,
    CreateShippingDetails,
    CreateShippingDetailsFromTemplate,
    EditShippingDetails,
    CreateTemplate,
    EditTemplate,
    CheckApproval,
}
