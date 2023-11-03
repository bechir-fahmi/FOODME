import { OrderAdjectiveVM } from "./OrderAdjectiveVM";
import { OrderElementVM } from "./OrderElementVM";

export class OrderModifierElementVM{
    id: number = 0;
    quantity : number = 0;
    idModifierElement : number = 0;
    version:number=0;
    modifierElements:OrderElementVM[];
    ModifierAdjectives:OrderAdjectiveVM[];
  }