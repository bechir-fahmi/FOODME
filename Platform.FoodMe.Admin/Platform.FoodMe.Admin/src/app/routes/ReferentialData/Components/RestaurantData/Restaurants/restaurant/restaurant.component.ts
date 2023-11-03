import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import { CreateOrEditRestaurantComponent } from '../create-or-edit-restaurant/create-or-edit-restaurant.component';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.scss']
})
export class RestaurantComponent implements OnInit {
  displayedColumns = ['name', 'contactName', 'contactPhone', 'contactMobile', 'contactEmail','addressLabelCode', 'latitude', 'longitude', 'brandId', 'areaId', 'isOpen', 'actions'];
  dataSource: MatTableDataSource<RestaurantVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private Restaurantservice: RestaurantService,private router: Router,private route: ActivatedRoute) {
  }
   Restaurants: RestaurantVM[] = [];

  ngOnInit() {
    this.getRestaurants();
  }

  editRestaurant(Restaurant: RestaurantVM)
  {
    this.router.navigate([
      'brandManagement/restaurants/createRestaurant',
      { page: 'create' },
    ]);

  }

  createRestaurant()
  {
    this.router.navigate([
      'brandManagement/restaurants/createRestaurant',
      { page: 'create' },
    ]);
  }
  viewRestaurant(Restaurant: RestaurantVM)
  {
    this.router.navigate([
      'brandManagement/restaurants/createRestaurant',
      { page: 'create' },
    ]);
  }

  refresh(): void {
    this.Restaurantservice.getAllRestaurants().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getRestaurants()
  {
    //send request to api and get responce
    this.Restaurantservice.getAllRestaurants().subscribe(res =>{
      this.Restaurants = res;
      this.dataSource = new MatTableDataSource(this.Restaurants);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteRestaurant(id: number){
    this.Restaurantservice.deleteRestaurant(id).subscribe(
      () => {
        this.refresh();
      });
  }

  getBrandName(object: any) {
    if (object) {
      return object.nameLanguageResources[0].value;
    }
  }
  getAreaName(object: any) {
    if (object) {
      return object.languageResources[0].value;
    }
  }
  getAdresse(object: any) {
    // if (object) {
    //   return object.addressLanguageResources[0].value;
    // }
  }
  getRestaurantName(object: any) {
    if (object) {
      return object.nameLanguageResources[0].value;
    }
  }

}
