import { DeliveryCompanyKey } from "./DeliveryCompanyKey";
import { DeliveryDriverVM } from "./DeliveryDriverVM";
import { OrderVM } from "./OrderVM";

export class DeliveryOrderVM{
    id: number = 0;
cancelled:boolean;
isMultipleServiceProviders:boolean;
nbrServiceProviders:number=0;
careemId:number=0;
status:string;
reference:string;
customer_id:number=0;
shop:string ;
branch:string;
branch_id:number=0;
branch_area:string;
dropoff_area:string;
tracking_code:string;
tracking_url:string;
expected_pickup:string;
at_pickup_at:string;
pickup_at:string;
at_dropoff_at:string;
dropoff_at:string;
fees:number=0;
distance:number=0;
status_id:number=0;
value:number=0;
payment_type:string;
details:string;
currency:string;
created_at:string;
client_order_id:string;
CustomerOrderId:number=0;
orderId:number=0;
YallowDriverId:number=0;
deliveryCompanyKey:DeliveryCompanyKey ;
deliveryDriverEnity:DeliveryDriverVM;
orderEntity:OrderVM;
}