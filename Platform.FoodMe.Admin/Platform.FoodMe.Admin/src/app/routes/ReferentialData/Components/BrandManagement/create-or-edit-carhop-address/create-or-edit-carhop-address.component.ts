import {AfterViewInit,Component, NgZone, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {ActivatedRoute, Router} from '@angular/router';
import {BrandVM} from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import {RestaurantVM} from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import {BrandService} from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';
import {RestaurantService} from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import {CarhopAddressService} from "../../../Services/BrandManagement/CarhopAddess.service";
import {FormControl, FormGroup} from "@angular/forms";
import {MapLoaderService} from "../../../../../map.loader";
import {PreloaderService} from "@core";
import {Guid} from "guid-typescript";
import * as https from "https";
import {HttpClient} from "@angular/common/http";
declare var google: any;
@Component({
  selector: 'app-create-or-edit-carhop-address',
  templateUrl: './create-or-edit-carhop-address.component.html',
  styleUrls: ['./create-or-edit-carhop-address.component.scss']
})
export class CreateOrEditCarhopAddressComponent implements OnInit {
  brandsName: String;
  CarhopAddressdescription: String;
  CarhopAddressrestaurant: String;
  CarhopAddressaddress: String;
  CarhopAddresslatitude: String;
  CarhopAddresslongitude: String;
  CarhopAddress: String;
  drawingManager: any;
  private map: google.maps.Map;
  private marker: google.maps.Marker;
  title?: string;
  action = 0;
  brands: BrandVM[] = [];
  brandsNames: String[] = [];
  restaurants: RestaurantVM[] = [];
  restaurantsNames: String[] = [];
  showMap = false;
  showMap1 = false;
  message: any;
  imagePath: any;
  url: any;
  msg: string;
  language: number = 1;
  selectedBrand?: string;
  selectedRestaurant?: string;

  selectedBrandId: number;
  selectedRestaurantId: number;
  private http: HttpClient;
  formValue = new FormGroup({
    selectedBrand: new FormControl(''),
    selectedRestaurant: new FormControl(''),
    CarhopAddress: new FormControl(''),
    CarhopAddresStatus: new FormControl(false),
    CarhopAddresslongitude: new FormControl(0),
    CarhopAddresslatitude: new FormControl(0),
    imageLabelCode: new FormControl(''),
    CarhopAddressdescription: new FormControl(''),

  })

  constructor(private preloader: PreloaderService,http: HttpClient, private zone: NgZone, public dialogref: MatDialogRef<CreateOrEditCarhopAddressComponent>, public carhopAddressService: CarhopAddressService, private route: ActivatedRoute, private router: Router, private Restaurantservice: RestaurantService, private BrandService: BrandService,
              @Inject(MAT_DIALOG_DATA) public data?: any) {
this.http=http
  }

  ngOnInit(): void {
    this.BrandService.getAllBrands().subscribe(res => {
      this.brands = res;
    })

    this.Restaurantservice.getAllRestaurants().subscribe(res => {
      this.restaurants = res
    })
    if (this.route.snapshot.url[this.route.snapshot.url.length - 2].toString().includes("update")) {
      this.title = 'Edit Carhop Address'
      // this.CarhopAddressService.getCarhopAddress(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.CarhopAddress=res});
    }
    if (this.route.snapshot.url[this.route.snapshot.url.length - 2].toString().includes("view")) {

      this.title = 'View Carhop Address'
      //  this.CarhopAddressService.getCarhopAddress(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.CarhopAddress=res});
    }
    if (this.route.snapshot.url[this.route.snapshot.url.length - 1].toString().includes("create")) {
      this.title = 'Add Carhop Address'
    }
  }


     convertImageToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const img = new Image();
        img.src = reader.result as string;
        img.onload = () => {
          const canvas = document.createElement('canvas');
          canvas.width = img.width;
          canvas.height = img.height;
          const ctx = canvas.getContext('2d');
          ctx!.drawImage(img, 0, 0);
          const base64 = canvas.toDataURL('image/jpeg');
          resolve(base64);
        };
        img.onerror = (error) => reject(error);
      };
      reader.onerror = (error) => reject(error);
    });
  }


  selectFile(event: any) {
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      this.msg = 'You must select an image';
      return;
    }

    var mimeType = event.target.files[0].type;

    if (mimeType.match(/image\/*/) == null) {
      this.msg = "Only images are supported";
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    var file=event.target.files[0];
    this.convertImageToBase64(file)
      .then((base64) => this.url = base64)
      .catch((error) => console.error(error));


  }


  createCarhopAddress() {
    const browserLang = navigator.language;
    const defaultlanguageKey = browserLang.match(/zh-CN/) ? 2 : browserLang.match(/zh-CN/) ? 3 : 1;

    let guidCode = Guid.create().toString();
    let descriptionLanguageResources = {
      code: Guid.create().toString(),
      value: this.formValue.value.CarhopAddressdescription,
      languageKey: defaultlanguageKey,

    }
    let imageFileResources ={
      code : Guid.create().toString(),
      value : this.formValue.value.imageLabelCode,
      languageKey : defaultlanguageKey,
    }
    let carhopAddress = {
      brandId: this.selectedBrandId,
      restaurantId: this.selectedRestaurantId,
      isActive: this.formValue.value.CarhopAddresStatus == false ? 0 : 1,
      latitude: Number(this.formValue.value.CarhopAddresslatitude),
      longtude: Number(this.formValue.value.CarhopAddresslongitude),
      imageFileLabelCode:imageFileResources.code,
      descriptionLabelCode: descriptionLanguageResources.code,
      descriptionLanguageResources: [
        descriptionLanguageResources
      ],
      imageFileResources: [
        imageFileResources
      ],
      fullAddress:this.formValue.value.CarhopAddress
    }
    this.carhopAddressService.addCarhopAddress(carhopAddress)
      .subscribe(res => {

          let ref = document.getElementById('cancel')
          ref?.click();
          this.formValue.reset();
        },
        err => {
          console.error(err);
        }
      )
  }


  ShowMap() {
    this.showMap = true;
    this.ngAfterViewInit();

  }

  ShowMap1() {
    this.showMap1 = true;
  }

  HideMap() {
    this.showMap = false;
  }

  HideMap1() {
    this.showMap1 = false;
  }

  myChange($event: any) {
    console.log('on myChange =>', $event);
    this.selectedBrand = $event.nameLanguageResources[0].value;
    this.selectedBrandId = $event.id;

  }

  myChangeRestaurant($event: any) {
    this.selectedRestaurant = $event.nameLanguageResources[0].value;
    this.selectedRestaurantId = $event.id;
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
      center: {lat: 21.488684387189114, lng: 50.18745789228833},
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
      const lng = marker.getPosition()?.lng();
      this.formValue.patchValue({
        CarhopAddresslatitude : lat,
        CarhopAddresslongitude : lng,

      })
    });

  }


}
