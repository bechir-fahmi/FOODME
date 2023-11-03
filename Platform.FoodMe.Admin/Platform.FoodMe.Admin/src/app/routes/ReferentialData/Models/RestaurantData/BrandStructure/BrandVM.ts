import { languageResourceVM } from "../../language-data/languageResourceVM";
import { CountryVM } from "../../location-data/CountryVM";
import { CompanyVM } from "../CompanyVM";
import { ContactVM } from "../ContactVM";
import { BrandGroupVM } from "./BrandGroupVM";
import { KitchenType } from "./KitchenType";
export class BrandVM {
  id:                           number;
  nameLabelCode:                string;
  descriptionLabelCode:         string;
  imageFileLabelCode:           string;
  coverImageFileLabelCode:      string;
  contact:                      null;
  webSite:                      null;
  tax:                          number;
  brandGroupId:                 null;
  descriptionLanguageResources: any[];
  imageFileResources:           languageResourceVM[];
  coverImageFileResources:      languageResourceVM[];
  brandDeliveryModes:           BrandDeliveryMode[];
  countryId:                    null;
  brandGroup :BrandGroupVM;
  country:CountryVM;
  licenseCode:string="";
  company:CompanyVM;
  contactNumber:number;
  contactMobile:number;
  contactEmail:string="";
  JahezMenuId:number=0;
  HungerStationChainCode:string;
  HungerStationMenuId:number=0;
  haveDeliveryService:boolean;
  haveDeliveryApplications:boolean;
  isActive:boolean;
  branchesNumber:number;
  kitchenType:KitchenType;
  JahezApiKey:string;
  JahezSecretCode:string;
  nameLanguageResources:languageResourceVM[];
}

export class BrandDeliveryMode {
  id:           number;
  deliveryType: number;
  brandId:      number;
}
