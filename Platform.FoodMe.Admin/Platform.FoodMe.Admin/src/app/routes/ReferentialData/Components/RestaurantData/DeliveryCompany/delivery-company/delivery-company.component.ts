import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DeliveryCompanyVM } from 'app/routes/ReferentialData/Models/RestaurantData/DeliveryCompanyVM';
import { DeliveryCompanyService } from 'app/routes/ReferentialData/Services/RestaurantData/DeliveryCompany.service';
import { CreateOrEditDeliveryCompanyComponent } from '../create-or-edit-delivery-company/create-or-edit-delivery-company.component';

@Component({
  selector: 'app-delivery-company',
  templateUrl: './delivery-company.component.html',
  styleUrls: ['./delivery-company.component.scss']
})
export class DeliveryCompanyComponent implements OnInit {
  displayedColumns = ['name', 'order', 'selectionTime','minimumDistance','maximumDistance','isActive', 'actions'];
  dataSource: MatTableDataSource<DeliveryCompanyVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private DeliveryCompanieservice: DeliveryCompanyService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   DeliveryCompanies: DeliveryCompanyVM[] = [];

  ngOnInit() {
    console.log(this.route);
    this.getDeliveryCompanies();
  }

  editDeliveryCompany(DeliveryCompany: DeliveryCompanyVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateDeliveryCompany/${DeliveryCompany.id}`)
    // this.router.navigate([navigateToEdit]);

  }

  createDeliveryCompany()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createDeliveryCompany");
    // this.router.navigate([navigateToCreate]);
  }

  viewDeliveryCompany(DeliveryCompany: DeliveryCompanyVM)
  {
    // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewDeliveryCompany/${DeliveryCompany.id}`)
    // this.router.navigate([navigateToView]);
  }

  refresh(): void {
    this.DeliveryCompanieservice.getAllDeliveryCompanies().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getDeliveryCompanies()
  {
    //send request to api and get responce
    this.DeliveryCompanieservice.getAllDeliveryCompanies().subscribe(res =>{
      this.DeliveryCompanies = res;
      this.dataSource = new MatTableDataSource(this.DeliveryCompanies);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteDeliveryCompany(id: number){
    this.DeliveryCompanieservice.deleteDeliveryCompany(id).subscribe(
      () => {
        this.refresh();
      });
  }

}


