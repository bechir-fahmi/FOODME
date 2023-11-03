import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import {CuisineService} from "../../../Services/CuisineService/Cuisine.service";
import {FormControl, FormGroup} from "@angular/forms";
import {MatTableDataSource} from "@angular/material/table";
import {CuisineDto} from "../../../Models/CuisineDto";
import { Guid } from 'guid-typescript';
import DevExpress from "devextreme";
import {base64} from "@core";
@Component({
  selector: 'app-create-or-edit-kitchen-type',
  templateUrl: './create-or-edit-kitchen-type.component.html',
  styleUrls: ['./create-or-edit-kitchen-type.component.scss']
})
export class CreateOrEditKitchenTypeComponent implements OnInit {
  title? : string;
  action = 0;
  url:any;
  page: number=0;
  size: number=5;
  msg:string;
  kitchenType:kitchenType;
  dataSource: MatTableDataSource<CuisineDto> = new MatTableDataSource<CuisineDto>;
  listCuisine: any;
  formValue = new FormGroup({
    nameLabelCode: new FormControl(''),
    imageLabelCode: new FormControl(''),
    kitchenStatus: new FormControl(false),

  })
 constructor( public dialogref: MatDialogRef<CreateOrEditKitchenTypeComponent>,private route: ActivatedRoute,private router: Router, public cuisineService:CuisineService,
    @Inject(MAT_DIALOG_DATA) public data?: any){

    }

  ngOnInit(): void {

    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
    {
     this.title = 'Edit Kitchen type'
      // this.CarhopAddressService.getCarhopAddress(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.CarhopAddress=res});
   }
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
   {

    this.title = 'View Kitchen type'
    //  this.CarhopAddressService.getCarhopAddress(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.CarhopAddress=res});
  }
  if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
       this.title = 'Add Kitchen type'
     }
   }
  loadData() {
    this.cuisineService.getCuisin(this.page,this.size).subscribe(
      (data: any) => {

        if (data) {
          this.listCuisine = data;
          this.dataSource = new MatTableDataSource(this.listCuisine)
        }
      }
    )
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

          console.log("base 64",base64)
          return base64;
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
      .then((base64) =>{
        this.url = base64;
        this.formValue.value.imageLabelCode= base64;
        console.log(" this.url",this.url)
      })

      .catch((error) => console.error(error));

  }

  createCuisine() {
    const browserLang = navigator.language;
    const defaultlanguageKey = browserLang.match(/zh-CN/) ? 2: browserLang.match(/zh-CN/) ? 3 : 1;

    let guidCode=Guid.create().toString();
    let nameLanguageResources ={
      code : Guid.create().toString(),
      value : this.formValue.value.nameLabelCode,
      languageKey : defaultlanguageKey,
    }
    let imageFileResources ={
      code : Guid.create().toString(),
      value : this.url,
      languageKey : defaultlanguageKey,
    }
    let cuisineDto = {
      status: this.formValue.value.kitchenStatus == false ? 0 : 1,
      nameLabelCode:nameLanguageResources.code,
      imageLabelCode:imageFileResources.code,
      imageFileResources: [
        imageFileResources
      ],
      nameLanguageResources: [
        nameLanguageResources
      ]
    };
    console.log("cuisineDto",cuisineDto)
    this.cuisineService.postCuisine(cuisineDto)
      .subscribe(res => {
          console.log(cuisineDto)
          let ref = document.getElementById('cancel')
          ref?.click();
          this.formValue.reset();
          this.loadData();
        },
        err => {
          console.error(err);
        }
      )
  }
  save() {
    let cuisine= {
      nameLabelCode: this.formValue.value.nameLabelCode,
      imageLabelCode: this.formValue.value.imageLabelCode
    }
    this.cuisineService.postCuisine(cuisine)
      .subscribe(res => {

          let ref = document.getElementById('cancel')
          ref?.click();

        },
        err => {
          console.error(err);
        }
      )
  }
}


export interface kitchenType {
  id:number;
  name : String;
}

