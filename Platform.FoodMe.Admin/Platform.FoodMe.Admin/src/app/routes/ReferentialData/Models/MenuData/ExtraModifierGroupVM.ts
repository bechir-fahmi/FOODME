import { languageResourceVM } from "../language-data/languageResourceVM";
import { AdjectiveVM } from "./AdjectiveVM";
import { MenuElementVM } from "./MenuElementVM";
import { ModifierItemVM } from "./ModifierItemVM";

export class ExtraModifierGroupVM{
    id:number=0;
    nameLabelCode : string = '';
    maxQuantityOfModifier:number=0;
    minRequiredQuantityOfModifier:number=0;
    element:MenuElementVM[];
    adjective:AdjectiveVM[];
    modifierItem:ModifierItemVM[];
    rank:number=0;
    languageResources:languageResourceVM[];
    menuItemId:number=0;
}