import { languageResourceVM } from "../language-data/languageResourceVM";
import { MenuCategoryVM } from "./MenuCategoryVM";
import { MenuTemplateVM } from "./MenuTemplateVM";
import { SubMenuVM } from "./SubMenuVM";

export class MenuVM{
    id: number = 0;
    nameLabelCode : string = '';
    isDefault:boolean;
   languageResources:languageResourceVM[];
 //  subMenusChildren:SubMenuVM[];
 //  menuCategories:MenuCategoryVM[];
   templateMenuId: number;
}