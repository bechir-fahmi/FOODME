import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CityVM } from 'app/routes/ReferentialData/Models/location-data/CityVM';
import { RegionVM } from 'app/routes/ReferentialData/Models/location-data/RegionVM';
import { CityService } from 'app/routes/ReferentialData/Services/location-data/city/city.service';
import { RegionService } from 'app/routes/ReferentialData/Services/location-data/region/region.service';

@Component({
  selector: 'app-create-or-edit-city-modal',
  templateUrl: './create-or-edit-city-modal.component.html',
  styleUrls: ['./create-or-edit-city-modal.component.scss']
})
export class CreateOrEditCityModalComponent implements OnInit {
  city: CityVM = new CityVM();
  title? : string;
  action = 0;

  constructor(private cityservice: CityService,private regionservice: RegionService, public dialogref: MatDialogRef<CreateOrEditCityModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    regions: RegionVM[] = [];
    
  ngOnInit(): void {
    this.regionservice.getRegions().subscribe(res =>{
        this.regions = res;
      });
    if(this.action == 1) {
      this.title = 'Edit City'
      this.city = this.data;
    }
    else {
      this.title = 'Add City'
    }
  }

  save(){
    if(this.action == 1) {
      this.cityservice.updateCity(this.city).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      this.cityservice.addCity(this.city).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }
}
