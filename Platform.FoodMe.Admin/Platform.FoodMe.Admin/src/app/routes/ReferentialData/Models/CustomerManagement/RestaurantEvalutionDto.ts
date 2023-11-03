
export interface RestaurantEvalutionDto {
  id: number;
  evalution: number;
  restaurantId: number;
  userId:string;
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
