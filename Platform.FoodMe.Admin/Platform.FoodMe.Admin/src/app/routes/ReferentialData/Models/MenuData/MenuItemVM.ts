import { DeliveryType } from "../Promotion/DeliveryType";
import { languageResourceVM } from "../language-data/languageResourceVM";
import { ExtraModifierGroupVM } from "./ExtraModifierGroupVM";
import { MenuElementVM } from "./MenuElementVM";
import { ModifierAdjectiveVM } from "./ModifierAdjectiveVM";
import { ModifierElementVM } from "./ModifierElementVM";
import { PricingPolicyVM } from "./PricingPolicyVM";
import { RelativeMenuElementsModifierGroupVM } from "./RelativeMenuElementsModifierGroupVM";
import { Visibility } from "./Visibility";

export class MenuItemVM{
    id: number = 0;
    nameLabelCode : string = '';
    descriptionLabelCode:string = '';
    priority:number=0;
    rank:number=0;
    price:number =0 ;
    menuCategoryId:number =0 ;
    imageLink: string = '';
    calories:number=0;
    pricingPolicy:PricingPolicyVM;
    visibility:Visibility;
    nameLanguageResources:languageResourceVM[];
    children:MenuElementVM[];
    extraModifierGroups:ExtraModifierGroupVM[];
    modifierByAdjectives:ModifierAdjectiveVM[];
    modifierByElements:ModifierElementVM[];
    relativeMenuElementsModifierGroups:RelativeMenuElementsModifierGroupVM[];
    deliveryType:DeliveryType;
}