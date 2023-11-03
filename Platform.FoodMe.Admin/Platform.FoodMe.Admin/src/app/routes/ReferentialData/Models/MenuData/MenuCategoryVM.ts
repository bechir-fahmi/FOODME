import { languageResourceVM } from "../language-data/languageResourceVM";
import { MenuElementVM } from "./MenuElementVM";
import { MenuItemVM } from "./MenuItemVM";
import { MenuVM } from "./MenuVM";
import { SubMenuVM } from "./SubMenuVM";

export class MenuCategoryVM{
    id: number = 0;
    nameLabelCode : string = '';
    descriptionLabelCode: string = '';
    priority:number=0;
    rank:number=0;
    menuId:number=0;
    parentMenu:MenuVM;
    parentSubMenu:SubMenuVM;
    categoryChildren:MenuCategoryVM[];
    parentCategory:MenuCategoryVM;
    elementChildren:MenuElementVM[];
    menuItemChildren:MenuItemVM[];
    nameLanguageResources:languageResourceVM[]
}