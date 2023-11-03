import { MenuCategoryVM } from "./MenuCategoryVM";
import { MenuVM } from "./MenuVM";
import { PricingPolicyVM } from "./PricingPolicyVM";

export class SubMenuVM{
    id: number = 0;
    nameLabelCode : string = '';
    descriptionLabelCode: string = '';
    pricingPolicy:PricingPolicyVM;
    menuParent:MenuVM[];
   subMenusChildren:SubMenuVM[];
   menuCategoriesChildren:MenuCategoryVM[];
}