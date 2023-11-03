import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { PromoBogoVM } from 'app/routes/ReferentialData/Models/Promotion/PromoBogoVM';
import {PromoBogoservice} from "../../../Services/PromoBogo.service";
import DevExpress from "devextreme";
import data = DevExpress.data;
import * as XLSX from "xlsx";

@Component({
  selector: 'app-promo-bogo',
  templateUrl: './promo-bogo.component.html',
  styleUrls: ['./promo-bogo.component.scss']
})
export class PromoBogoComponent implements OnInit {
  displayedColumns = ['name','activationDate','expirationDate','pricingType', 'status','actions'];
  dataSource: MatTableDataSource<PromoBogoVM>;
  listPromoBogo:any;
  page:number=0;
  size:number=5;
  showInputFiltre: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private router: Router,private route: ActivatedRoute ,public  promoBogoservice:PromoBogoservice ) {
  }
  ngOnInit() {
    this.loadData();
  }

  editPromoBogo(PromoBogo: PromoBogoVM)
  {
    this.router.navigate([
      'promotions/promoBogo/createPromoBogo',
      {page :"edit", my_object: JSON.stringify(PromoBogo) },
    ]);
  }

  createPromoBogo()
  {
    this.router.navigate([
      'promotions/promoBogo/createPromoBogo',
      { page: 'create' },
    ]);
  }

  viewPromoBogo(PromoBogo: PromoBogoVM)
  {
    this.router.navigate([
      'promotions/promoBogo/createPromoBogo',
      {page :"view", my_object: JSON.stringify(PromoBogo) },
    ]);
  }
  loadData() {
    this.promoBogoservice.getPromoBogo(this.page,this.size).subscribe(
      (data: any) => {
        if (data) {
          this.listPromoBogo = data;
          this.dataSource = new MatTableDataSource(this.listPromoBogo)
        }
      }
    )
  }
  deletePromoBogo(id: number){
    this.promoBogoservice.deletePromoBogo(id).subscribe(
      () => {
        this.loadData();
      });
  }

  exportExcel() {
    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listPromoBogo);
    const workbook: XLSX.WorkBook = {Sheets: {'listPromoBogo': worksheet}, SheetNames: ['listPromoBogo']};
    const excelBuffer: any = XLSX.write(workbook, {bookType: 'xlsx', type: 'array'});
    this.saveAsExcelFile(excelBuffer, 'listPromoBogo');
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {"type": 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
    const url: string = window.URL.createObjectURL(data);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = fileName + '.xlsx';
    link.click();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}





