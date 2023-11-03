import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BrandGroupVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandGroupVM';
import { BrandGroupService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/BrandGroup.service';

@Component({
  selector: 'app-brand-groups',
  templateUrl: './brand-groups.component.html',
  styleUrls: ['./brand-groups.component.scss']
})
export class BrandGroupsComponent implements OnInit {
  displayedColumns = ['name', 'phoneNumber', 'email', 'actions'];
  dataSource: MatTableDataSource<BrandGroupVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private BrandGroupservice: BrandGroupService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   BrandGroups: BrandGroupVM[] = [];

  ngOnInit() {
    console.log(this.route);
    this.getBrandGroups();
  }

  editBrandGroup(BrandGroup: BrandGroupVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateBrandGroup/${BrandGroup.id}`)
    // this.router.navigate([navigateToEdit]);

  }

  createBrandGroup()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createBrandGroup");
    // this.router.navigate([navigateToCreate]);
  }

  viewBrandGroup(BrandGroup: BrandGroupVM)
  {
    // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewBrandGroup/${BrandGroup.id}`)
    // this.router.navigate([navigateToView]);
  }

  refresh(): void {
    this.BrandGroupservice.getAllBrandGroups().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getBrandGroups()
  {
    //send request to api and get responce
    this.BrandGroupservice.getAllBrandGroups().subscribe(res =>{
      this.BrandGroups = res;
      this.dataSource = new MatTableDataSource(this.BrandGroups);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteBrandGroup(id: number){
    this.BrandGroupservice.deleteBrandGroup(id).subscribe(
      () => {
        this.refresh();
      });
  }

}

