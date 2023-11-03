import { OrderAdjectiveVM } from "./OrderAdjectiveVM";
import { OrderElementVM } from "./OrderElementVM";

export class OrderExtraModifierVM{
    id: number = 0;
    quantity : number = 0;
    idExtraModifier : number = 0;
    version:number=0;
    modifierElements:OrderElementVM[];
    ModifierAdjectives:OrderAdjectiveVM[];
  }