import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DeliveryOfferVM } from 'app/routes/ReferentialData/Models/Promotion/DeliveryOfferVM';

@Component({
  selector: 'app-delivery-offer',
  templateUrl: './delivery-offer.component.html',
  styleUrls: ['./delivery-offer.component.scss']
})
export class DeliveryOfferComponent implements OnInit {
  displayedColumns = ['name', 'startDate', 'endDate','amount','isActive', 'actions'];
  dataSource: MatTableDataSource<DeliveryOfferVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute) {
  }
   DeliveryOffers: DeliveryOfferVM[] = [];

  ngOnInit() {
    this.getDeliveryOffers();
  }

  editDeliveryOffer(DeliveryOffer: DeliveryOfferVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateMenu/${DeliveryOffer.id}`)
    // this.router.navigate([navigateToEdit]);
  }

  createDeliveryOffer()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createDeliveryOffer");
    // this.router.navigate([navigateToCreate]);
  }

  viewDeliveryOffer(DeliveryOffer: DeliveryOfferVM)
  {
    // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenu/${DeliveryOffer.id}`)
    // this.router.navigate([navigateToView]);
  }







  refresh(): void {
    // this.DeliveryOfferservice.getAllDeliveryOffers().subscribe(res =>{
    //   this.dataSource.data = res;
    // });
  }
  getDeliveryOffers()
  {
    this.dataSource = new MatTableDataSource(this.DeliveryOffers);
    //send request to api and get responce
    // this.DeliveryOfferservice.getAllDeliveryOffers().subscribe(res =>{
    //   this.DeliveryOffers = res;
    //   this.dataSource = new MatTableDataSource(this.DeliveryOffers);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // });
  }
  deleteDeliveryOffer(id: number){
    // this.DeliveryOfferservice.deleteDeliveryOffer(id).subscribe(
    //   () => {
    //     this.refresh();
    //   });
  }

}




