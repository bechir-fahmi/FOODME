import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LanguageVM } from 'app/routes/ReferentialData/Models/language-data/LanguageVM';
import { LanguageService } from 'app/routes/ReferentialData/Services/LanguageData/language.service';

@Component({
  selector: 'app-create-or-edit-language-modal',
  templateUrl: './create-or-edit-language-modal.component.html',
  styleUrls: ['./create-or-edit-language-modal.component.scss']
})
export class CreateOrEditLanguageModalComponent implements OnInit {
  language: LanguageVM = new LanguageVM();
  title? : string;
  action = 0;

  constructor(private languageservice: LanguageService, public dialogref: MatDialogRef<CreateOrEditLanguageModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    languages: LanguageVM[] = [];
    
  ngOnInit(): void {
    this.languageservice.getLanguages().subscribe(res =>{
        this.languages = res;
      });
    if(this.action == 1) {
      this.title = 'Edit Language'
      this.language = this.data;
    }
    else {
      this.title = 'Add Language'
    }
  }

  save(){
    if(this.action == 1) {
      this.languageservice.updateLanguage(this.language).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      this.languageservice.addLanguage(this.language).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }

}
