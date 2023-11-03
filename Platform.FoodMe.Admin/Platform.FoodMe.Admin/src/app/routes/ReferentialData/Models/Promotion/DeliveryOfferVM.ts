import {PromoType} from "./PromoType";
import {CouponAssignementDTO} from "./CouponVM";

export class DeliveryOfferVM{
  id: number = 0;

  name : string = '';
  activationDate : Date;
  expirationDate : Date;
  pricingType:PromoType;
  deliveryAmount: number;
  isActive:boolean;
  assignee?:CouponAssignementDTO;
}
