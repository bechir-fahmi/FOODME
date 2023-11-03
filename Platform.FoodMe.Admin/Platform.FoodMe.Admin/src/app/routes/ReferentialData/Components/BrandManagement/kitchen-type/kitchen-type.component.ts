import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { KitchenType } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/KitchenType';
import { CuisineDto } from '../../../Models/CuisineDto';
import { CuisineService } from '../../../Services/CuisineService/Cuisine.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-kitchen-type',
  templateUrl: './kitchen-type.component.html',
  styleUrls: ['./kitchen-type.component.scss'],
})
export class KitchenTypeComponent implements OnInit {
  displayedColumns = ['nameLabelCode', 'imageLabelCode', 'status', 'actions'];
  dataSource: MatTableDataSource<CuisineDto> = new MatTableDataSource<CuisineDto>();
  page: number = 0;
  size: number = 5;
  listCuisine: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  showInputFiltre: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public cuisineService: CuisineService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadData();
  }
  loadData() {
    this.cuisineService.getCuisin(this.page, this.size).subscribe((data: any) => {
      if (data) {
        this.listCuisine = data;
        this.dataSource = new MatTableDataSource(this.listCuisine);
      }
    });
  }
  loadDataPaginated(page: number, size: number) {
    this.cuisineService.getCuisin(page, size).subscribe(data => {
      this.listCuisine = data;
      this.dataSource = new MatTableDataSource(this.listCuisine);
    });
  }
  pageChanged($event: PageEvent) {
    this.loadDataPaginated($event.pageIndex, $event.pageSize);
    this.paginator.pageSize = $event.pageSize;
    this.paginator.pageIndex = $event.pageIndex;
  }

  editKitchenType(KitchenType: KitchenType) {
    // let navigateToEdit = this.route.snapshot._routerState.url.concat(
    //   `/updateKitchenType/${KitchenType}`
    // );
    // this.router.navigate([navigateToEdit]);
  }

  createKitchenType() {
    this.router.navigate([
      'brandManagement/kitchenTypes/createKitchenType',
      { page: 'create' },
    ]);
  }

  viewKitchenType(KitchenType: KitchenType) {
    // let navigateToView = this.route.snapshot._routerState.url.concat(
    //   `/viewKitchenType/${KitchenType}`
    // );
    // this.router.navigate([navigateToView]);
  }

  deleteKitchenType(id: number) {
    this.cuisineService.deleteCuisine(id).subscribe(() => {
      this.loadDataPaginated(this.page, this.size);
    });
  }
  getName(object: any) {
    if (object) {
      return object.nameLanguageResources[0].value;
    }
  }


  convertBase64ToImage(base64: string): Promise<HTMLImageElement> {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.onload = () => resolve(img);
      img.onerror = error => reject(error);
      img.src = base64;
    });
  }

  getImage(object: any) {
    if (object) {
      return object.imageFileResources[0].value;
    }
  }
  saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    });
    const url: string = window.URL.createObjectURL(data);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = fileName + '.xlsx';
    link.click();
  }
  exportExcel(): void {
    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listCuisine);
    const workbook: XLSX.WorkBook = {
      Sheets: { listCuisine: worksheet },
      SheetNames: ['listCarhopAddress'],
    };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, 'listCuisine');
  }


}


