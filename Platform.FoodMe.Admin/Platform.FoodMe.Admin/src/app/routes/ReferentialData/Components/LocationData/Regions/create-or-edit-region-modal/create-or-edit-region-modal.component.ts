import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CountryDTO } from '@shared/Models/LocationData/CountryDto';
import { CountryService } from '@shared/services/LocationService/country.service';
import { RegionVM } from 'app/routes/ReferentialData/Models/location-data/RegionVM';
import { RegionService } from 'app/routes/ReferentialData/Services/location-data/region/region.service';


@Component({
  selector: 'app-create-or-edit-region-modal',
  templateUrl: './create-or-edit-region-modal.component.html',
  styleUrls: ['./create-or-edit-region-modal.component.scss']
})
export class CreateOrEditregionModalComponent implements OnInit{

  region: RegionVM = new RegionVM();
  title? : string;
  action = 0;

  constructor(private regionservice: RegionService,private countryservice: CountryService, public dialogref: MatDialogRef<CreateOrEditregionModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    countries: CountryDTO[] = [];
    
  ngOnInit(): void {
    this.countryservice.getcountries().subscribe(res =>{
        this.countries = res;
      });
    if(this.action == 1) {
      this.title = 'Edit region'
      this.region = this.data;
    }
    else {
      this.title = 'Add region'
    }
  }

  save(){
    if(this.action == 1) {
      this.regionservice.updateRegion(this.region).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      this.regionservice.addRegion(this.region).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }
}
