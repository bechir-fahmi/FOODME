import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CountryVM } from 'app/routes/ReferentialData/Models/location-data/CountryVM';
import { BrandGroupVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandGroupVM';
import { BrandVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { KitchenType } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/KitchenType';
import { CompanyVM } from 'app/routes/ReferentialData/Models/RestaurantData/CompanyVM';
import { ContactVM } from 'app/routes/ReferentialData/Models/RestaurantData/ContactVM';
import { CountryService } from 'app/routes/ReferentialData/Services/location-data/country/country.service';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';
import { BrandGroupService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/BrandGroup.service';
import { ContactService } from 'app/routes/ReferentialData/Services/RestaurantData/Contact.service';

@Component({
  selector: 'app-create-or-edit-brand-modal',
  templateUrl: './create-or-edit-brand-modal.component.html',
  styleUrls: ['./create-or-edit-brand-modal.component.scss']
})
export class CreateOrEditBrandModalComponent implements OnInit {
  Brand: BrandVM = new BrandVM();
  title? : string;
  view=false;
  brandGroups:BrandGroupVM[]=[];
  countries:CountryVM[]=[];
  contacts:ContactVM[]=[];
  companies:CompanyVM[]=[];
  kitchenTypes:any[]=[];

  constructor(private Brandservice: BrandService,private BrandGroupservice: BrandGroupService,private contactService: ContactService,private countryservice: CountryService, public dialogref: MatDialogRef<CreateOrEditBrandModalComponent>,private route: ActivatedRoute,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      const kitchenTypes = Object.keys(KitchenType)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: KitchenType[name as keyof typeof KitchenType],
          name,
        };
      });
      this.kitchenTypes=kitchenTypes;
    }

  ngOnInit(): void {
this.BrandGroupservice.getAllBrandGroups().subscribe(res=>{this.brandGroups=res})
this.contactService.getAllContacts().subscribe(res=>{this.contacts=res})
this.countryservice.getcountries().subscribe(res=>{this.countries=res})
    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
   {
    this.title = 'Edit Brand'
     this.Brandservice.getBrand(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.Brand=res});
  }
  if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
  {
    this.view=true;
   this.title = 'View Brand'
    this.Brandservice.getBrand(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.Brand=res});
 }
 if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
      this.title = 'Add Brand'
    }
  }

  save(){
    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update")) {

      this.Brandservice.updateBrand(this.Brand).subscribe(
        () => {
          this.router.navigate(["/brandManagement/brands"]);
        }
      )
    }
    else {

      this.Brandservice.addBrand(this.Brand).subscribe(
        () => {
          this.router.navigate(["/brandManagement/brands"]);
        }
      )
    }
  }

}

