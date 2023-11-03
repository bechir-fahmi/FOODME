import { languageResourceVM } from "../language-data/languageResourceVM";
import { RegionVM } from "./RegionVM";

export class CityVM {
    id: number = 0;
    nameLabelCode: string = '';
    regionId: number=0;
    code: string = '';
    countryKey: string = '';
    latitude:number=0;
    longitude:number=0;
    sDMId: number | null = 0;
    languageResources: languageResourceVM[];

}




