import { PricingPolicyVM } from "./PricingPolicyVM";
import { Visibility } from "./Visibility";

export class MenuElementVM{
    id: number = 0;
    nameLabelCode : string = '';
    descriptionLabelCode:string = '';
    priority:number=0;
    imageLink: string = '';
    calories:number=0;
    Quantity:number=0;
    rank:number=0;
    price:number=0;
    pricingPolicy:PricingPolicyVM;
    visibility:Visibility;
}