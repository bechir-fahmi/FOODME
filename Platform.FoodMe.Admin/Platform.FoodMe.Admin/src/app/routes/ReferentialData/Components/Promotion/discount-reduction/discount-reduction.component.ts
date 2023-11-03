import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DiscountReductionVM } from 'app/routes/ReferentialData/Models/Promotion/DiscountReductionVM';
import {DiscountReductionservice} from "../../../Services/DiscountReduction.service";
import * as XLSX from "xlsx";

@Component({
  selector: 'app-discount-reduction',
  templateUrl: './discount-reduction.component.html',
  styleUrls: ['./discount-reduction.component.scss']
})
export class DiscountReductionComponent implements OnInit {
  displayedColumns = ['name', 'activationDate','expirationDate','pricingType','discount','status','actions'];
  dataSource: MatTableDataSource<DiscountReductionVM>;
  listDiscountReduction:any;
  page:number=0;
  size:number=5;
  showInputFiltre: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute,public discountReductionService: DiscountReductionservice) {
  }

  ngOnInit() {
    this.loadData();
  }

  editDiscountReduction(DiscountReduction: DiscountReductionVM)
  {
    this.router.navigate([
      'promotions/discountReduction/createDiscountReduction',
      {page :"edit", my_object: JSON.stringify(DiscountReduction) },
    ]);
  }

  createDiscountReduction()
  {
    this.router.navigate([
      'promotions/discountReduction/createDiscountReduction',
      { page: 'create' },
    ]);
  }

  viewDiscountReduction(DiscountReduction: DiscountReductionVM)
  {
    this.router.navigate([
      'promotions/discountReduction/createDiscountReduction',
      {page :"view", my_object: JSON.stringify(DiscountReduction) },
    ]);
  }


  loadData() {
    this.discountReductionService.getDiscountReduction(this.page,this.size).subscribe(
      (data: any) => {
        if (data) {
          this.listDiscountReduction = data;
          this.dataSource = new MatTableDataSource(this.listDiscountReduction)
        }
      }
    )
  }
  deleteDiscountReduction(id: number){
    this.discountReductionService.deleteDiscountReduction(id).subscribe(
      () => {
        this.loadData();
      });
  }
  exportExcel() {
    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listDiscountReduction);
    const workbook: XLSX.WorkBook = {Sheets: {'listDiscountReduction': worksheet}, SheetNames: ['listDiscountReduction']};
    const excelBuffer: any = XLSX.write(workbook, {bookType: 'xlsx', type: 'array'});
    this.saveAsExcelFile(excelBuffer, 'listDiscountReduction');
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {"type": 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
    const url: string = window.URL.createObjectURL(data);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = fileName + '.xlsx';
    link.click();
  }
}





