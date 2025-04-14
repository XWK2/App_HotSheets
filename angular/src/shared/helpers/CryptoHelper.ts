import * as cryptoJS from 'crypto-js';
import { AppConsts } from '../AppConsts';

export class CryptoHelper {
    static encrypt(value: any): string {
        let encryptedValue = cryptoJS.AES.encrypt(value.toString(), AppConsts.cryptoJS.secretKey).toString();
        return encryptedValue;
    }

    static decrypt(value: string): string {
        let bytes = cryptoJS.AES.decrypt(value, AppConsts.cryptoJS.secretKey);
        let originalText = bytes.toString(cryptoJS.enc.Utf8);
        return originalText;
    }
}
