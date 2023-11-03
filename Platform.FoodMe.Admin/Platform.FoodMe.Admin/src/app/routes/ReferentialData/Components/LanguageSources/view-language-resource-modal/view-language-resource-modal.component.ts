import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LanguageResourceDto } from 'app/routes/ReferentialData/Models/language-data/language-resource-dto';

@Component({
  selector: 'app-view-language-resource-modal',
  templateUrl: './view-language-resource-modal.component.html',
  styleUrls: ['./view-language-resource-modal.component.scss']
})
export class ViewLanguageResourceModalComponent implements OnInit{
  languageRecource: LanguageResourceDto = new LanguageResourceDto();

  constructor(public dialogref: MatDialogRef<ViewLanguageResourceModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){}

  ngOnInit(): void {
    this.languageRecource = this.data;
  }
}
