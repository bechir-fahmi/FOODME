import { languageResourceVM } from "../language-data/languageResourceVM";
import { CountryVM } from "./CountryVM";

export class RegionVM {
    id: number = 0;
    nameLabelCode: string = '';
    countryId: number=0;
    posRegionId: number=0;
    sDMId: number | null = 0;
    languageResources:  languageResourceVM[];

}
/*
{
    "id": 0,
    "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "countryId": 0,
    "posRegionId": 0,
    "languageResources": [
      {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "value": "string",
        "languageKey": 0
      }
    ]
  }*/ 