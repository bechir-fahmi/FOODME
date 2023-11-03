import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BrandVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';
import { CreateOrEditBrandModalComponent } from '../create-or-edit-brand-modal/create-or-edit-brand-modal.component';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {

  displayedColumns = ['nameLabelCode', 'descriptionLabelCode','brandGroup', 'contact', 'webSite', 'imageFileLabelCode', 'country', 'licenseCode', 'tax', 'actions'];
  dataSource: MatTableDataSource<BrandVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private Brandservice: BrandService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   Brands: BrandVM[] = [];

  ngOnInit() {
    this.getBrands();
  }

  editBrand(Brand: BrandVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateBrand/${Brand.id}`)
    // this.router.navigate([navigateToEdit]);

  }

  createBrand()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createBrand");
    // this.router.navigate([navigateToCreate]);
  }

  viewBrand(Brand: BrandVM)
  {
    // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewBrand/${Brand.id}`)
    // this.router.navigate([navigateToView]);
  }

  refresh(): void {
    this.Brandservice.getAllBrands().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getBrands()
  {
    //send request to api and get responce
    this.Brandservice.getAllBrands().subscribe(res =>{
      this.Brands = res;
      this.dataSource = new MatTableDataSource(this.Brands);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteBrand(id: number){
    this.Brandservice.deleteBrand(id).subscribe(
      () => {
        this.refresh();
      });
  }

}

