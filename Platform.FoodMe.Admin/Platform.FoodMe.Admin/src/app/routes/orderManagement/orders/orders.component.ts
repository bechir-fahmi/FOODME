import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { OrderFilterVM } from 'app/routes/ReferentialData/Models/OrderManagement/OrderFilterVM';
import { OrderVM } from 'app/routes/ReferentialData/Models/Promotion/OrderVM';
import { BrandVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { DeliveryCompanyVM } from 'app/routes/ReferentialData/Models/RestaurantData/DeliveryCompanyVM';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { CityVM } from 'app/routes/ReferentialData/Models/location-data/CityVM';
import { RegionVM } from 'app/routes/ReferentialData/Models/location-data/RegionVM';
import { OrderService } from 'app/routes/ReferentialData/Services/OrderManagement/order.service';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';
import { DeliveryCompanyService } from 'app/routes/ReferentialData/Services/RestaurantData/DeliveryCompany.service';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';
import { CityService } from 'app/routes/ReferentialData/Services/location-data/city/city.service';
import { RegionService } from 'app/routes/ReferentialData/Services/location-data/region/region.service';
import { CardOrderComponent } from './card-order/card-order.component';
import { OrderStatus } from 'app/routes/ReferentialData/Models/Promotion/OrderStatus';
import { TrackingOrderComponent } from './tracking-order/tracking-order.component';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';



@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})

export class OrdersComponent implements OnInit  {


  setClasses = new Set();
  public isCollapsed = true;
  restaurants: RestaurantVM[] = [];
  regions :  RegionVM [] = [];
 brands: BrandVM[] = [];
   cities : CityVM[] =[];
   users:UserVM[]=[];
   orders : OrderVM[]=[];
   isFiltreShowed : boolean = false;
 //  orderFilter: OrderFilterVM[]=[];
   devliveryCompanies: DeliveryCompanyVM[]= [];
   range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  })
  orderFilter : OrderFilterVM = new OrderFilterVM();

   public    orderStatusSelect: any[] = [

    {value: 0, viewValue: 'Initial'},
    {value: 1, viewValue: 'PendingPayment'},
    {value: 2, viewValue: 'Received'},
    {value: 3, viewValue: 'FindingDriver'},
    {value: 4, viewValue: 'DriverAccept'},
    {value: 5, viewValue: 'Inkitchen'},
    {value: 6, viewValue: 'Manual'},
    {value: 9, viewValue: 'Delivered'},
    {value: 10, viewValue: 'Closed'},
    {value: 11, viewValue: 'Canceled'},
    {value: 12, viewValue: 'ForceCancel'},
    {value: 13, viewValue: 'ForceClosed'},
    {value: 65, viewValue: 'Arrived'},
    {value: 14, viewValue: 'BUMPED'},
    {value: 15, viewValue: 'Ready'},
    {value: 16, viewValue: 'ASSIGNED'},
    {value: 32, viewValue: 'ENROUTE'},
    {value: 96, viewValue: 'Suspended'},
    {value: 100, viewValue: 'FUTURE'},
    {value: 256, viewValue: 'FAILURE'},
    {value: 1024, viewValue: 'UNKNOWN'},
    {value: 4096, viewValue: 'REQUESTFORCANCELORDER'},
  ];


  
  public    orderTypeSelect: any[] = [

    {value: 1, viewValue: 'Delivery'},
    {value: 2, viewValue: 'Pickup'},
    {value: 3, viewValue: 'Carhop'},
    {value: 4, viewValue: 'Dining'},

  ];

  public paymentModeSelect: any[] = [

    {value: 1, viewValue: 'Cash'},
    {value: 2, viewValue: 'CreditCard'},
    {value: 3, viewValue: 'ApplePay'},
    {value: 4, viewValue: 'Wallet'},

  ];

  public paymentStatusSelect: any[] = [
    {value: 0, viewValue: 'NotPaid'},
    {value: 1, viewValue: 'Paid'},
  ];
   //public fileTypes = Object.values(OrderStatus);
 constructor(private restaurantservice :RestaurantService, private regionService :RegionService,  private  cityService: CityService, private deliveryCompanyservice :DeliveryCompanyService
 , private userService: UserService ,  private orderService : OrderService, private brandService :BrandService, public dialog: MatDialog){

 }
  ngOnInit(): void {

    this.orderService.getAllOrder().subscribe(res=>{

this.orders =res;
    });
   /*   this.restaurantservice.getAllRestaurants().subscribe(res=>{
      this.restaurants=res ;
    }); */
    this.brandService.getAllBrands().subscribe(res=>{
      this.brands = res ; 
    }); 
    this.regionService.getRegions().subscribe(res=>{
      this.regions = res ; 
    }); 
    this.cityService.getCities().subscribe(res=>{
this.cities =res;
    });

    this.deliveryCompanyservice.getAllDeliveryCompanies().subscribe(res=>{
this.devliveryCompanies =res;
    })
  
    this.userService.getAllUsers().subscribe(res=>{this.users=res;});
 
  }

  eventRestaurantId( event:any){
      this.orderFilter.orderRestaurantFilter = event.value;    
  }
  eventRegionId( event:any){

    this.orderFilter.orderRegionFilter = event.value;   
}
eventCityId( event:any){
  this.orderFilter.orderCityFilter = event.value;   
}
eventOrderStatusId( event:any){
  this.orderFilter.orderStatusFilter = event.value;   
}

eventDeliveryTypeId( event:any){
  this.orderFilter.deliveryTypeFilter = event.value;   
}
eventPayementTypeId( event:any){
  this.orderFilter.paymentModeFilter = event.value;   
}
eventPayementStatusId( event:any){
  this.orderFilter.paymentStatusFilter = event.value;   
}
eventAgentId( event:any){
  this.orderFilter.agentIdFilter = event.value;   
}
companyIdFilter( event:any){
  this.orderFilter.orderCompanyFilter = event.value;   
}

eventBrandId( event :any){
 this.restaurantservice.getRestaurantsByBrandId(event.value).subscribe(res=>{
  this.restaurants=res ;
  this.orderFilter.orderBrandFilter= event.value;
 });
}
  save(){


this.orderFilter;
this.orderService.geFilterOrders(this.orderFilter).subscribe (res=>{
  console.log(res);
})
    
  }

  showFiltre(){
    this.isFiltreShowed = !this.isFiltreShowed; 
      }

      getLabel(object:any) {
        if (object){
          return object.nameLanguageResources[0].value;
        }
      
      }
      getViewValuePaymentMode(id:number ){
        return this.paymentModeSelect.find(({ value }) => value === id).viewValue;
       
       }

       

       getViewValuePaymentStatus( id:number ){
        return this.paymentStatusSelect.find(({ value }) => value === id).viewValue;
       }

       getViewValueOrderStatus( id:number ){
        return this.orderStatusSelect.find(({ value }) => value === id).viewValue;
       }
       getdevliveryCompanies( id:number ){
        let deliveryCompany = null;
        if(this.devliveryCompanies){
          deliveryCompany = this.devliveryCompanies.find((devliv) => devliv.deliveryCompanyKey === id);
        }

       }
       openPopupCart(order :any){
        this.dialog.open(CardOrderComponent,{
          width:'1300px',
          height:'1000px',
          data: { order: JSON.stringify(order)  } 
        });
      }

      openPopupTrack (order :any){
        this.dialog.open(TrackingOrderComponent,{
          width:'1300px',
          height:'1000px',
          data: { order: JSON.stringify(order)  } 
        });
      }

 
    isVisibleForcedClosedButton( order :OrderVM){

return (order.orderStatus != OrderStatus.Closed && order.orderStatus !=  OrderStatus.ForceClosed && order.orderStatus != OrderStatus.ForceCancel && order.orderStatus != OrderStatus.Canceled ) 
    }

    //*ngIf="record.order.orderMode == orderMode.Delivery && (record.order.orderStatus == orderStatus.Received || record.order.orderStatus == orderStatus.FindingDriver)"
    isVisibleForcedCancelButton( order :OrderVM){

 return /*order.orderMode == == orderMode.Delivery*/ (order.orderStatus ==  OrderStatus.Received || order.orderStatus == OrderStatus.FindingDriver)
  }

  //*ngIf="!(record.order.orderMode == orderMode.Delivery && (record.order.orderStatus == orderStatus.Received || record.order.orderStatus == orderStatus.FindingDriver)) && 
 // (record.order.orderStatus != orderStatus.Closed && record.order.orderStatus != orderStatus.ForceClosed && record.order.orderStatus != orderStatus.Canceled && record.order.orderStatus != orderStatus.ForceCancel)"

 //Resend
 /*isVisibleResendButton(order:OrderVM){
  return ( /*(order.orderMode == orderMode.Delivery (order.orderStatus ==  OrderStatus.Received || order.orderStatus ==  OrderStatus.FindingDriver)) && 
  (order.orderStatus !=  OrderStatus.Closed && order.orderStatus != OrderStatus.ForceClosed)
 }*/
    changeColorLigneTable( order :OrderVM){
      if (order.total > 200){
        this.setClasses.add("backgroud-ligne-order-yellow");
      } /*else if (order.smid>0){
        this.setClasses.add("backgroud-ligne-order-red");
      }*/
      else{
        this.setClasses = new Set();
      }
   
      return true;
    }


    
}
