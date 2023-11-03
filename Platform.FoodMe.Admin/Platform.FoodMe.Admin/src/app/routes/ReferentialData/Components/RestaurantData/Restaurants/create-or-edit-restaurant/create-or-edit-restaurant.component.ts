import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AreaVM } from 'app/routes/ReferentialData/Models/location-data/AreaVM';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';
import { DeliveryType } from 'app/routes/ReferentialData/Models/Promotion/DeliveryType';
import { BrandVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { DeliveryCompanyVM } from 'app/routes/ReferentialData/Models/RestaurantData/DeliveryCompanyVM';
import { RestaurantType } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantType';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { AreaService } from 'app/routes/ReferentialData/Services/location-data/area/area.service';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';
import { DeliveryCompanyService } from 'app/routes/ReferentialData/Services/RestaurantData/DeliveryCompany.service';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import { MenuService } from 'app/routes/ReferentialData/Services/MenuData/Menu.service';
import {CityService} from "../../../../Services/location-data/city/city.service";
import {CityVM} from "../../../../Models/location-data/CityVM";

@Component({
  selector: 'app-create-or-edit-restaurant',
  templateUrl: './create-or-edit-restaurant.component.html',
  styleUrls: ['./create-or-edit-restaurant.component.scss']
})
export class CreateOrEditRestaurantComponent implements OnInit {
  Restaurant: RestaurantVM = new RestaurantVM();
  title? : string;
  view=false;
  areas:AreaVM[]=[];
  brands:BrandVM[]=[];
  cityVM:CityVM[]=[];
  RestaurantTypes:any[]=[];
  DeliveryTypes:any[]=[];
  deliveryCompanies:DeliveryCompanyVM[]=[];
  menus:MenuVM[]=[];
  showMap=false;
  lat = 51.678418;
  lng = 7.809007;

 constructor(private Restaurantservice: RestaurantService,private areaservice: AreaService,private cityService:CityService,
  private brandservice: BrandService,public dialogref: MatDialogRef<CreateOrEditRestaurantComponent>,private router: Router
  ,private route: ActivatedRoute,private deliveryCompanyService:DeliveryCompanyService,private menuService:MenuService,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      const RestaurantTypes = Object.keys(RestaurantType)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: RestaurantType[name as keyof typeof RestaurantType],
          name,
        };
      });
      this.RestaurantTypes=RestaurantTypes;

      const DeliveryTypes = Object.keys(DeliveryType)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: DeliveryType[name as keyof typeof DeliveryType],
          name,
        };
      });
      this.DeliveryTypes=DeliveryTypes;
    }

  ngOnInit(): void {
    this.menuService.getAllMenus().subscribe(res=>{
      this.menus=res;
      })
      this.deliveryCompanyService.getAllDeliveryCompanies().subscribe(res=>{
       this.deliveryCompanies=res;
      })
   this.areaservice.getAreas().subscribe(res=>{
   this.areas=res;
   })
   this.brandservice.getAllBrands().subscribe(res=>{
    this.brands=res;
   })
    this.cityService.getCities().subscribe(res=>{
      this.cityVM=res;
    })
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
   {
    this.title = 'Edit Restaurant'
     this.Restaurantservice.getRestaurant(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.Restaurant=res});
  }
  if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
  {
    this.view=true;
   this.title = 'View Restaurant'
    this.Restaurantservice.getRestaurant(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.Restaurant=res});
 }
 if (this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
      this.title = 'Add Restaurant'
    }
  }
  ShowMap(){
    this.showMap=true;

  }
  HideMap(){
    this.showMap=false;
  }
  save(){

    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update")) {

      this.Restaurantservice.updateRestaurant(this.Restaurant).subscribe(
        () => {
          this.router.navigate(["/brandManagement/restaurants"]);
        }
      )
    }
    else {

      this.Restaurantservice.addRestaurant(this.Restaurant).subscribe(
        () => {
          this.router.navigate(["/brandManagement/restaurants"]);
        }
      )
    }
  }



}

