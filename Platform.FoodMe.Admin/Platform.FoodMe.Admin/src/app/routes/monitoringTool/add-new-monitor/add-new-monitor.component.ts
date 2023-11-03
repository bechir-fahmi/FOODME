import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderFilterVM } from 'app/routes/ReferentialData/Models/OrderManagement/OrderFilterVM';
import { OrderVM } from 'app/routes/ReferentialData/Models/Promotion/OrderVM';
import { BrandVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { DeliveryCompanyVM } from 'app/routes/ReferentialData/Models/RestaurantData/DeliveryCompanyVM';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { CityVM } from 'app/routes/ReferentialData/Models/location-data/CityVM';
import { RegionVM } from 'app/routes/ReferentialData/Models/location-data/RegionVM';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';
import { DeliveryCompanyService } from 'app/routes/ReferentialData/Services/RestaurantData/DeliveryCompany.service';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';
import { CityService } from 'app/routes/ReferentialData/Services/location-data/city/city.service';
import { RegionService } from 'app/routes/ReferentialData/Services/location-data/region/region.service';
import { MonitorService } from 'app/routes/ReferentialData/Services/monitoringToolsData/monitor.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-new-monitor',
  templateUrl: './add-new-monitor.component.html',
  styleUrls: ['./add-new-monitor.component.scss']
})
export class AddNewMonitorComponent implements OnInit {
  public isCollapsed = true;
  filteredOptions: Observable<any[]>;
  myControl = new FormControl<string | any>('');
  orderFilter :OrderFilterVM = new OrderFilterVM();
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  })

  setClasses = new Set();
  restaurants: RestaurantVM[] = [];
  regions :  RegionVM[] = [];
 brands: BrandVM[] = [];
   cities : CityVM[] =[];
   users:UserVM[]=[];
   orders : OrderVM[]=[];
   isFiltreShowed : boolean = false;
 //  orderFilter: OrderFilterVM[]=[];
   devliveryCompanies: DeliveryCompanyVM[]= [];

  constructor(private monitorService :MonitorService,
    private router: Router, 
    private restaurantservice : RestaurantService,
    private brandService: BrandService, 
    private regionService:RegionService,
    private cityService: CityService,
    private deliveryCompanyservice:DeliveryCompanyService,
    private userService: UserService ){

  }
 ngOnInit(): void {
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
 save()
{


  this.monitorService.addOrderFilter(this.orderFilter).subscribe(
    () => {
     this.router.navigate(["monitoring-tool/we-care/monitor-list"]);
    console.log(this.orderFilter);
    }
  )
  this.orderFilter;
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

  displayFn(user: any): string {
    return user && user.name ? user.name : '';
  }
}
