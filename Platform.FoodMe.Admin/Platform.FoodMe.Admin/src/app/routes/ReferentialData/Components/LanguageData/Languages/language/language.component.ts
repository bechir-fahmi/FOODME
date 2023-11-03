import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { LanguageVM } from 'app/routes/ReferentialData/Models/language-data/LanguageVM';
import { LanguageService } from 'app/routes/ReferentialData/Services/LanguageData/language.service';
import { CreateOrEditLanguageModalComponent } from '../create-or-edit-language-modal/create-or-edit-language-modal.component';

@Component({
  selector: 'app-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.scss']
})
export class LanguageComponent implements OnInit {
  displayedColumns = ['name', 'actions'];
  dataSource: MatTableDataSource<LanguageVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private Languageservice: LanguageService,
    public dialog: MatDialog) {
  }
  languages: LanguageVM[] = [];

  ngOnInit() {
    this.getLanguages();
  }
  getLanguages()
  {
    this.Languageservice.getLanguages().subscribe(res =>{
      this.languages = res;
      this.dataSource = new MatTableDataSource(this.languages);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  editLanguage(language: LanguageVM)
  {
    let dialogRef = this.dialog.open(CreateOrEditLanguageModalComponent, {
      data: language
    });
    dialogRef.componentInstance.action = 1;
  }

  createLanguage()
  {
    this.dialog.afterAllClosed.subscribe(()=> this.refresh());
    this.dialog.open(CreateOrEditLanguageModalComponent);
  }

  viewLanguage(language: LanguageVM)
  {
    this.dialog.open(CreateOrEditLanguageModalComponent, {
      data: language
    });
  }

  refresh(): void {
    this.Languageservice.getLanguages().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  deleteLanguage(id: number){
    this.Languageservice.deleteLanguage(id).subscribe(
      () => {
        this.refresh();
      });
  }
}
