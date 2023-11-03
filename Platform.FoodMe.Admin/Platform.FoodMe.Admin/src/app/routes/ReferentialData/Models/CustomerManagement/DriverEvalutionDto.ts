
export interface DriverEvalutionDto {
  id: number;
  evalution: number;
  driverName: string;
  driverPhone:string;
  userId:number;
  orderId:number;
  customerUnlikeReasons:CustomerUnlikeReasons[];
}
export interface CustomerUnlikeReasons {
  id:number;
  description:string;
  userId:number;
  orderId:number;
  unlikeReasonId:number;
}
