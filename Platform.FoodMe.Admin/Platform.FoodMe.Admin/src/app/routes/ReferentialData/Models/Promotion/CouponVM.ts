import {CouponType} from "./CouponType";
import {RestaurantVM} from "../RestaurantData/RestaurantVM";
import {CustomerVM} from "./CustomerVM";
import {DiscountType} from "./DiscountType";

export class CouponVM{


  id: number = 0;
  name:string;
  code:string;
  type:CouponType;
  category:number;
  discount:DiscountType;
  discountPercentage:number=0;
  activationDate:Date;
  expirationDate:Date;
  status:boolean;
  couponConditions?:CouponConditionDTO;
  assignee?:CouponAssignementDTO;
  dontApplyLoyality:boolean;
  dontApplyOffer:boolean;
  sunday:boolean;
  monday:boolean;
  tuesday:boolean;
  wednesday:boolean;
  thursday:boolean;
  friday:boolean;
  saturday:boolean;

  }
export interface CouponConditionDTO {
  id: number;
  code: string;
}
export interface CouponAssignementDTO {
  id: number;
  code: string;
}
