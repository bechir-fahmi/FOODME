import { PaymentMode } from "../Promotion/PaymentMode";
import { languageResourceVM } from "../language-data/languageResourceVM";

export class PaymentTypeVM {
    id: number = 0;
    nameLabelCode: string = '';
    languageResources:languageResourceVM[];
    paymentMode:PaymentMode;
}

