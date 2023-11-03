import {AfterViewInit, Component, NgZone,Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AreaVM } from 'app/routes/ReferentialData/Models/location-data/AreaVM';
import { CityVM } from 'app/routes/ReferentialData/Models/location-data/CityVM';
import { AreaService } from 'app/routes/ReferentialData/Services/location-data/area/area.service';
import { CityService } from 'app/routes/ReferentialData/Services/location-data/city/city.service';
import { PreloaderService } from '@core';
import { MapLoaderService } from 'app/map.loader';
declare var google: any;
@Component({
  selector: 'app-create-or-edit-area-modal',
  templateUrl: './create-or-edit-area-modal.component.html',
  styleUrls: ['./create-or-edit-area-modal.component.scss']
})
export class CreateOrEditAreaModalComponent implements OnInit, AfterViewInit  {
  area: AreaVM = new AreaVM();
  title? : string;
  action = 0;
 // map: any;
  drawingManager: any;
  private map: google.maps.Map;
  private marker: google.maps.Marker;

  constructor(private preloader: PreloaderService,private areaservice: AreaService,private cityservice: CityService, public dialogref: MatDialogRef<CreateOrEditAreaModalComponent>,private zone: NgZone,
              @Inject(MAT_DIALOG_DATA) public data?: any){}
  cities: CityVM[] = [];
  ngOnInit(): void {
    console.log(this.area)
    this.cityservice.getCities().subscribe(res =>{
      this.cities = res;
    });
    if(this.action == 1) {
      this.title = 'Edit Area'
      this.area = this.data;
    }
    else {
      this.title = 'Add Area'
    }
  }
  save(){
    if(this.action == 1) {
      this.areaservice.updateArea(this.area).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      this.areaservice.addArea(this.area).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }
  ngAfterViewInit() {
    this.preloader.hide();
    this.zone.runOutsideAngular(() => {
      MapLoaderService.load().then(() => {
        this.drawPolygon();
      });
    });
  }
  drawPolygon() {
    this.map = new google.maps.Map(document.getElementById('map'), {
      center: { lat: 21.488684387189114, lng: 50.18745789228833},
      zoom: 8,
    });
    this.drawingManager = new google.maps.drawing.DrawingManager({
      drawingMode: google.maps.drawing.OverlayType.MARKER,
      drawingControl: true,
      drawingControlOptions: {
        position: google.maps.ControlPosition.TOP_CENTER,
        drawingModes: [
          google.maps.drawing.OverlayType.MARKER
        ]
      },
    });
    this.drawingManager.setMap(this.map);
    google.maps.event.addListener(this.drawingManager, 'markercomplete', (marker: google.maps.Marker) => {
      const lat = marker.getPosition()?.lat();
      const lang = marker.getPosition()?.lng();
      console.log("lat  "+lat+" lang "+lang);
    });
  }

}
