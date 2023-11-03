import { languageResourceVM } from "../language-data/languageResourceVM";
import { BrandGroupVM } from "../RestaurantData/BrandStructure/BrandGroupVM";
import { BrandVM } from "../RestaurantData/BrandStructure/BrandVM";
import { MenuStatus } from "./MenuStatus";
import { MenuType } from "./MenuType";
import { MenuVM } from "./MenuVM";

export class MenuTemplateVM{
   id: number = 0;
   nameLabelCode : string = '';
   languageResources:languageResourceVM[];
   menuType: number = 0;
   menuStatus: number = 0;
  // children:MenuVM[];
   brandId:number =  0;
 //  brandGroupParent:BrandGroupVM;

}

/*
{
   "id": 0,
   "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
   "languageResources": [
     {
       "id": 0,
       "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
       "value": "string",
       "languageKey": 0
     }
   ],
   "brandId": 0,
   "menuType": 0,
   "menutStatus": 0
 }
 */
