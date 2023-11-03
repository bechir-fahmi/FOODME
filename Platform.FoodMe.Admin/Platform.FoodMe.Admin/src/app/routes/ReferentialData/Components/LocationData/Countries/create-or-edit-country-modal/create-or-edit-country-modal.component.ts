import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { languageResourceVM } from 'app/routes/ReferentialData/Models/language-data/languageResourceVM';
import { CountryVM } from 'app/routes/ReferentialData/Models/location-data/CountryVM';
import { LanguageResourceService } from 'app/routes/ReferentialData/Services/LanguageData/language-resource.service';
import { CountryService } from 'app/routes/ReferentialData/Services/location-data/country/country.service';
import { Subscription } from 'rxjs';
import { CreateOrEditLanguageResourceModalComponent } from '../../../LanguageSources/create-or-edit-language-resource-modal/create-or-edit-language-resource-modal.component';
import { ViewLanguageResourceModalComponent } from '../../../LanguageSources/view-language-resource-modal/view-language-resource-modal.component';

@Component({
  selector: 'app-create-or-edit-country-modal',
  templateUrl: './create-or-edit-country-modal.component.html',
  styleUrls: ['./create-or-edit-country-modal.component.scss']
})
export class CreateOrEditCountryModalComponent implements OnInit{

  country: CountryVM = new CountryVM();
  title? : string;
  action = 0;
  displayedColumns = ['languageKey','name', 'actions'];
  dataSource: languageResourceVM[]=[];
  languages: languageResourceVM[] = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  message:string;
  subscription: Subscription;
  
  constructor(private countryservice: CountryService,@Optional() public dialogref: MatDialogRef<CreateOrEditCountryModalComponent>,
    public dialog: MatDialog,private languageResourceservice: LanguageResourceService,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    
  ngOnInit(): void {
   
      
    if(this.action == 1) {
      this.title = 'Edit Country'
      this.country = this.data;
      this.getLanguageResources();
    }
    else {
      this.title = 'Add Country';
      this.country.code = "00000000-0000-0000-00000000000";
        this.message=this.country.code;
        this.languageResourceservice.sendCountryCode(this.message)
        
    }
  }

  save(){
    if(this.action == 1) {
      this.countryservice.updatecountry(this.country).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      this.countryservice.addCountry(this.country).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }

  editLanguageResource(languageResource: languageResourceVM)
  {
    let dialogRef = this.dialog.open(CreateOrEditLanguageResourceModalComponent, {
      data: languageResource
    });
    dialogRef.componentInstance.action = 1;
  }

  createLanguageResource()
  {
    this.dialog.afterAllClosed.subscribe(()=> this.refresh());
    this.dialog.open(CreateOrEditLanguageResourceModalComponent);
  }

  viewLanguageResource(languageResource: languageResourceVM)
  {
    this.dialog.open(ViewLanguageResourceModalComponent, {
      data: languageResource
    });
  }

  refresh(): void {
    if(this.country.code != "00000000-0000-0000-00000000000")
    {
      this.languageResourceservice.getLanguageResourcesByCountryCode(this.country.code).subscribe(res =>{
        this.dataSource= res;
      });
    }
  }
  getLanguageResources()
  {
    //send request to api and get responce
    this.languageResourceservice.getLanguageResources().subscribe(res =>{
      this.languages = res;
    });
  }
  deleteLanguageResource(id: number){
    this.languageResourceservice.deleteLanguageResource(id).subscribe(
      () => {
        this.refresh();
      });
  }
}
