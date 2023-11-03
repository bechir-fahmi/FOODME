import { OrderAdjectiveVM } from "./OrderAdjectiveVM";
import { OrderElementVM } from "./OrderElementVM";

export class OrderModifierAdjectiveVM{
    id: number = 0;
    quantity : number = 0;
    idModifierAdjective : number = 0;
    version:number=0;
    modifierElements:OrderElementVM[];
    ModifierAdjectives:OrderAdjectiveVM[];
  }