import { OrderElementVM } from "./OrderElementVM";
import { OrderExtraModifierVM } from "./OrderExtraModifierVM";
import { OrderModifierAdjectiveVM } from "./OrderModifierAdjectiveVM";
import { OrderModifierElementVM } from "./OrderModifierElementVM";

export class OrderItemVM{
    id: number = 0;
    quantity: number = 0;
    idChooseableItem: number = 0;
    version: number = 0;
    name : string = '';
    elements:OrderElementVM[];
    modifierElements:OrderModifierElementVM[];
    modifierAdjectives:OrderModifierAdjectiveVM[];
    extraModifiers:OrderExtraModifierVM[];
  }