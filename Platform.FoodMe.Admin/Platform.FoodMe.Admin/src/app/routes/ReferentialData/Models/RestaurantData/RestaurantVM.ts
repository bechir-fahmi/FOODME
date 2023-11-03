import { MenuVM } from "../MenuData/MenuVM";
import { DeliveryType } from "../Promotion/DeliveryType";
import { languageResourceVM } from "../language-data/languageResourceVM";
import { DeliveryCompanyVM } from "./DeliveryCompanyVM";
import { RestaurantType } from "./RestaurantType";

export class RestaurantVM{
    id = 0;
    nameLabelCode  = '';
    contactName  = '';
    contactPhone  = '';
    contactMobile  = '';
    contactEmail  = '';
    addressLabelCode  = '';
    latitude:number;
    longitude:number;
    brandId:number;
    areaId:number;
    sDM_Store_ID:number;
    sDM_Store_Num  = '';
    sDM_Menu_ID:number;
    startTime:Date;
    endTime:Date;
    isOpen:boolean;
    type:RestaurantType;
    address:string;
    deliveryType:DeliveryType;
    deliveryAmount:number;
    deliveryMinAmount:number;
    deliveryAreaKm:number;
    pickUpTime:number;
    takerProfileId:number;
    isHungerStation:boolean;
    isActive:boolean;
    menu:MenuVM;
    deliveryCompany:DeliveryCompanyVM;
    nameLanguageResources: languageResourceVM[];
}
  export interface DeliveryMode {
  id:              number;
  waitingTimeFrom: number;
  waitingTimeTo:   number;
  ammount:         number;
  minAmmount:      number;
  maxAmmount:      number;
  distanceKM:      number;
  deliveryType:    number;
  restaurant:      null;
}

export interface NameLanguageResource {
  id:          number;
  code:        string;
  value:       string;
  languageKey: number;
}
export interface addressLanguageResources {
  id:          number;
  code:        string;
  value:       string;
  languageKey: number;
}
