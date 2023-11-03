import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { CarhopAddressVM } from 'app/routes/ReferentialData/Models/BrandManagement/CarhopAddressVM';
import { CarhopAddressService } from '../../../Services/BrandManagement/CarhopAddess.service';
import { BrandVM } from '../../../Models/RestaurantData/BrandStructure/BrandVM';
import { RestaurantVM } from '../../../Models/RestaurantData/RestaurantVM';
import { RestaurantService } from '../../../Services/RestaurantData/Restaurant.service';
import { BrandService } from '../../../Services/RestaurantData/BrandStructure/Brand.service';
import * as XLSX from 'xlsx';
import { RestourantService } from '@shared/services/RestaurantService/restaurant.service';

@Component({
  selector: 'app-carhop-address',
  templateUrl: './carhop-address.component.html',
  styleUrls: ['./carhop-address.component.scss'],
})
export class CarhopAddressComponent implements OnInit {
  page: number = 0;
  size: number = 5;
  brands: BrandVM[] = [];
  restaurants: RestaurantVM[] = [];
  Filter: CarhopAddressVM = new CarhopAddressVM();
  showInputFiltre: boolean;
  listCarhopAddress: any;
  displayedColumns = [
    'brand',
    'restaurant',
    'address',
    'descriptionLabelCode',
    'latitude',
    'longitude',
    'isActive',
    'imageLocation',
    'actions',
  ];
  dataSource: MatTableDataSource<CarhopAddressVM>;
  selectedOption: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public restaurantService: RestaurantService,
    public restourantService: RestourantService,
    public carhopAddressService: CarhopAddressService,
    public Restaurantservice: RestaurantService,
    public brandService: BrandService
  ) {}
  CarhopAddresses: CarhopAddressVM[] = [];

  ngOnInit(): void {

    this.loadData();
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  loadData() {
    this.carhopAddressService.getCarhopAddresss(this.page, this.size).subscribe((data: any) => {
      if (data) {
        this.listCarhopAddress = data;
        this.dataSource = new MatTableDataSource(this.listCarhopAddress);
      }
    });
  }
  eventRestaurantId(event: any) {
    this.Filter.restaurantId = event.value;
  }
  eventBrandId(event: any) {
    this.restaurantService.getRestaurantsByBrandId(event.value).subscribe(res => {
      this.restaurants = res;
      this.Filter.brandId = event.value;
    });
  }
  pageChanged($event: PageEvent) {
    this.loadDataPaginated($event.pageIndex, $event.pageSize);
    this.paginator.pageSize = $event.pageSize;
    this.paginator.pageIndex = $event.pageIndex;
  }
  loadDataPaginated(page: number, size: number) {
    this.carhopAddressService.getCarhopAddresss(page, size).subscribe(data => {
      this.listCarhopAddress = data;
      this.dataSource = new MatTableDataSource(this.listCarhopAddress);
    });
  }

  editCarhopAddress(CarhopAddress: CarhopAddressVM) {
    this.router.navigate([
      'brandManagement/carhopRestaurants/createCarhopAddress',
      { page: 'create' },
    ]);
  }
  createCarhopAddress() {
    this.router.navigate([
      'brandManagement/carhopRestaurants/createCarhopAddress',
      { page: 'create' },
    ]);
  }

  viewCarhopAddress(CarhopAddress: CarhopAddressVM) {
    this.router.navigate([
    'brandManagement/carhopRestaurants/createCarhopAddress',
    { page: 'create' },
  ]);
  }

  deleteCarhopAddress(id: number) {
    this.carhopAddressService.deleteCarhopAddress(id).subscribe(() => {
      this.loadData();
    });
  }
  getBrandName(object: any) {
    if (object) {
      return object.nameLanguageResources[0].value;
    }
  }
  getRestaurantName(object: any) {
    if (object) {
      return object.nameLanguageResources[0].value;
    }
  }
  getDescription(object: any) {
    if (object) {
      return object.descriptionLanguageResources[0].value;
    }
  }
  getImage(object: any) {
    if (object) {
      return object.imageFileResources[0].value;
    }
  }
  exportExcel(): void {
    let worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.listCarhopAddress);
    const workbook: XLSX.WorkBook = {
      Sheets: { listCarhopAddress: worksheet },
      SheetNames: ['listCarhopAddress'],
    };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, 'listCarhopAddress');
  }
  convertBase64ToImage(base64: string): Promise<HTMLImageElement> {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.onload = () => resolve(img);
      img.onerror = error => reject(error);
      img.src = base64;
    });
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
}
