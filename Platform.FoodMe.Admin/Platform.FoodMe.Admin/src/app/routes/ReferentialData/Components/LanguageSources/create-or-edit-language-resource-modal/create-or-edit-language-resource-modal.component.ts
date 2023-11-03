import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LanguageResourceDto } from 'app/routes/ReferentialData/Models/language-data/language-resource-dto';
import { LanguageKey } from 'app/routes/ReferentialData/Models/language-data/languageKey';
import { languageResourceVM } from 'app/routes/ReferentialData/Models/language-data/languageResourceVM';
import { LanguageResourceService } from 'app/routes/ReferentialData/Services/LanguageData/language-resource.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-create-or-edit-language-resource-modal',
  templateUrl: './create-or-edit-language-resource-modal.component.html',
  styleUrls: ['./create-or-edit-language-resource-modal.component.scss']
})
export class CreateOrEditLanguageResourceModalComponent  implements OnInit {
  languageResource: languageResourceVM = new languageResourceVM();
  title? : string;
  action = 0;
  languageKeys:string[]=[];
  countryCode:string;
  constructor(private languageResourceservice: LanguageResourceService, public dialogref: MatDialogRef<CreateOrEditLanguageResourceModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    languages: LanguageResourceDto[] = [];
    message:string;
    subscription: Subscription;
    
  ngOnInit(): void {
    
    this.languageResourceservice.getCountryCode().subscribe(info => {
      this.countryCode = info;
      
    })
    this.languageKeys = Object.keys(LanguageKey).filter(f => isNaN(Number(f)));
    if(this.action == 1) {
      this.title = 'Edit Language Resource'
      this.languageResource = this.data;
    }
    else {
      this.title = 'Add Language Resource'
      
    }
  }

  save(){
    if(this.action == 1) {
      this.languageResourceservice.updateLanguageResource(this.languageResource).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
  this.languageResource.code=this.countryCode;
  console.log(this.languageResource)
      this.languageResourceservice.addLanguageResource(this.languageResource).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }

}
