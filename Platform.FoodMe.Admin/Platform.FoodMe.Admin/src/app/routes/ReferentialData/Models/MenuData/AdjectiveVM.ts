import { Visibility } from "./Visibility";

export class AdjectiveVM{
    id: number = 0;
    nameLabelCode : string = '';
    descriptionLabelCode: string = '';
    priority:number=0;
    rank:number=0;
    visibility:Visibility;
    version:number=0;
    imageLink: string = '';
    calories:number=0;
    quantity:number=0;
}