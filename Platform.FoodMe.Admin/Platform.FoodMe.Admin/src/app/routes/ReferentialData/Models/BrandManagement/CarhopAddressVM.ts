
export class CarhopAddressVM {
    id: number = 0;
    brandId:number;
    restaurantId:number;
  imageFileLabelCode?:string;
     fullAddress?:string;
    descriptionLabelCode:string;
    descriptionLanguageResources:    descriptionLanguageResources[];
  imageFileResources:    imageFileResources[];
    isActive:number;
    latitude:number;
    longtude:number;

}
export interface descriptionLanguageResources {
  id:          number;
  code:        string;
  value:       string;
  languageKey: number;
}
export interface imageFileResources {
  id:          number;
  code:        string;
  value:       string;
  languageKey: number;
}
