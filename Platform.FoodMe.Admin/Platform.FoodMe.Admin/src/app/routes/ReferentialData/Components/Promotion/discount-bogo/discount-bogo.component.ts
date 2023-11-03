import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DiscountBogoVM } from 'app/routes/ReferentialData/Models/Promotion/DiscountBogoVM';

@Component({
  selector: 'app-discount-bogo',
  templateUrl: './discount-bogo.component.html',
  styleUrls: ['./discount-bogo.component.scss']
})
export class DiscountBogoComponent implements OnInit {


  displayedColumns = ['name', 'isActive','actions'];
  dataSource: MatTableDataSource<DiscountBogoVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute) {
  }
   DiscountBogos: DiscountBogoVM[] = [];

  ngOnInit() {
    this.getDiscountBogos();
  }

  editDiscountBogo(DiscountBogo: DiscountBogoVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateMenu/${DiscountBogo.id}`)
    // this.router.navigate([navigateToEdit]);
  }

  createDiscountBogo()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createDiscountBogo");
    // this.router.navigate([navigateToCreate]);
  }

  viewDiscountBogo(DiscountBogo: DiscountBogoVM)
  {
    // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenu/${DiscountBogo.id}`)
    // this.router.navigate([navigateToView]);
  }







  refresh(): void {
    // this.DiscountBogoservice.getAllDiscountBogos().subscribe(res =>{
    //   this.dataSource.data = res;
    // });
  }
  getDiscountBogos()
  {
    this.dataSource = new MatTableDataSource(this.DiscountBogos);
    //send request to api and get responce
    // this.DiscountBogoservice.getAllDiscountBogos().subscribe(res =>{
    //   this.DiscountBogos = res;
    //   this.dataSource = new MatTableDataSource(this.DiscountBogos);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // });
  }
  deleteDiscountBogo(id: number){
    // this.DiscountBogoservice.deleteDiscountBogo(id).subscribe(
    //   () => {
    //     this.refresh();
    //   });
  }

}







