import { languageResourceVM } from "../language-data/languageResourceVM";

export class DeliveryCompanyVM{
    id: number = 0;
    nameLabelCode : string = '';
    selectionTime:number = 0;
    order : number = 0;
    minDistance: number = 0;
    maxDistance: number = 0;
    isActive:number=0;
    deliveryCompanyKey:number=0;
    languageResources:languageResourceVM[];
  }