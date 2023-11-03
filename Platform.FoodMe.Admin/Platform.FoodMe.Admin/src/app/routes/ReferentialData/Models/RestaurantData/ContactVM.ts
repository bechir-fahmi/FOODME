import { languageResourceVM } from "../language-data/languageResourceVM";

export class ContactVM{
    id: number = 0;
    nameLabelCode : string = '';
    email : string = '';
    phoneNumber : string = '';
    mobileNumber : string = '';
    languageResources : languageResourceVM[] =[];
  }