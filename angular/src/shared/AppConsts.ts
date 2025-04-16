export class AppConsts {
    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish
    static helpDocumentUrl: string;
    static templateNPUrl: string;
    static hotSheetsPrintUrl: string;
    static wsPortalHotSheetsUrl: string;

    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin',
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'HotSheet',
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token',
    };

    static readonly cryptoJS = {
        secretKey: 'd3ns0',
    };
}
