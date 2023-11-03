import { PromoType } from "./PromoType";
import {CouponAssignementDTO} from "./CouponVM";

export class PromoBogoVM{
    id: number = 0;
    name : string;
    activationDate : Date;
    expirationDate:Date;
    pricingType:PromoType;
    status:boolean;
    fixPrice:number;
    assignee?:CouponAssignementDTO;

  }
