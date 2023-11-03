import { TypeOfGettingPoints } from "./TypeOfGettingPoints";

export class CustomerPointVM {
    id: number = 0;
    name:string;
    orderId:number=0;
    points:number=0;
    earnedPointBy:TypeOfGettingPoints;
}