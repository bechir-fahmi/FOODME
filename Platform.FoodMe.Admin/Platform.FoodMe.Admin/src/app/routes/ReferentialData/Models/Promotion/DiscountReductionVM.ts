import {PromoType} from "./PromoType";
export class DiscountReductionVM{
  id: number = 0;
  name: string = '';
  activationDate:Date;
  expirationDate:Date;
  pricingType : PromoType;
  status:boolean;
  discount:number;
  arDescription?:string;
  enDescription?:string;
  Sunday:boolean;
  Monday:boolean;
  Tuesday:boolean;
  Wednesday:boolean;
  Thursday:boolean;
  Friday:boolean;
  Saturday:boolean;
  conditions?:conditions[];
  assignee?:assignee[];
}
export class conditions {

}
  export class assignee {

  }
