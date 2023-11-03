import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DeliveryZoneVM } from 'app/routes/ReferentialData/Models/BrandManagement/DeliveryZoneVM';

@Component({
  selector: 'app-delivery-zone',
  templateUrl: './delivery-zone.component.html',
  styleUrls: ['./delivery-zone.component.scss']
})
export class DeliveryZoneComponent implements OnInit {

  displayedColumns = ['name','minimumDeliveryCharge','maximumDeliveryCharge','deliveryChargePerKm','maximumCodOrderAmount', 'actions'];
  dataSource: MatTableDataSource<DeliveryZoneVM>;
  DeliveryZones:any[]=[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
     
  }


  ngOnInit() {
    this.getDeliveryZones();
  }

  editDeliveryZone(DeliveryZone: DeliveryZoneVM)
  {
   // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateDeliveryZone/${DeliveryZone}`)
    //this.router.navigate([navigateToEdit]);
    
  }

  createDeliveryZone()
  {
  // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createDeliveryZone");
   this.router.navigate(["brandManagement/deliveryZone/createDeliveryZone"]);
  }

  viewDeliveryZone(DeliveryZone: DeliveryZoneVM)
  {
   // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewDeliveryZone/${DeliveryZone}`)
    // this.router.navigate([navigateToView]);
  }

  refresh(): void {
   
  }
  getDeliveryZones()
  {
    this.dataSource = new MatTableDataSource(this.DeliveryZones);
    //send request to api and get responce
    // this.DeliveryZoneservice.getAllDeliveryZones().subscribe(res =>{
    //   this.DeliveryZones = res;
    //   this.dataSource = new MatTableDataSource(this.DeliveryZones);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // });
  }
  deleteDeliveryZone(id: number){
    // this.DeliveryZoneservice.deleteDeliveryZone(id).subscribe(
    //   () => {
    //     this.refresh();
    //   });
  }

}



