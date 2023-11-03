import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import { Brand, Restaurant } from '../../Models/Brand';
import { AnyARecord } from 'dns';
import { BusinessHoursService } from '../../business-hours.service';

@Component({
  selector: 'app-calendars-list',
  templateUrl: './calendars-list.component.html',
  styleUrls: ['./calendars-list.component.scss'],
})
export class CalendarsListComponent implements OnInit {
  displayedColumns = ['name', 'description', 'actions'];
  dataSource: MatTableDataSource<RestaurantVM>;
  workingTimes: any;
  selectedItems = [];
  selectedBrands: Brand[] = [];
  brands: Brand[] = [
    {
      name: 'Brand1',
      selected: false,
      restaurants: [
        { name: 'Restaurant1', selected: false },
        { name: 'Restaurant2', selected: false },
        { name: 'Restaurant3', selected: false },
      ],
    },
    {
      name: 'Brand2',
      selected: false,
      restaurants: [
        { name: 'Restaurant4', selected: false },
        { name: 'Restaurant5', selected: false },
        { name: 'Restaurant6', selected: false },
      ],
    },
    {
      name: 'Brand3',
      selected: false,
      restaurants: [
        { name: 'Restaurant7', selected: false },
        { name: 'Restaurant8', selected: false },
        { name: 'Restaurant9', selected: false },
      ],
    },
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private Restaurantservice: RestaurantService,
    private businessHoursService: BusinessHoursService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
  Restaurants: RestaurantVM[] = [];
  ngOnInit(): void {
    this.getRestaurants();
  }
  public isCollapsed = true;

  getRestaurants() {
    // send request to api and get responce
    this.Restaurantservice.getAllRestaurants().subscribe(res => {
      this.Restaurants = res;
      this.dataSource = new MatTableDataSource(this.Restaurants);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });

    // this.businessHoursService.getAllWorkingTime().subscribe(res => {
    //    this.workingTimes = res;
    // });
  }

  //filter
  isSelected(item: any): boolean {
    // return this.selectedItems.indexOf() > -1;
    return true;
  }

  onSelect(): void {
    console.log(this.selectedItems);
  }

  selectedRestaurants: Restaurant[] = [];
  selectedRestaurantNames: string[] = [];

  updateSelectedBrands(item: Brand, isChecked: boolean) {
    if (isChecked) {
      this.selectedBrands.push(item);
    } else {
      const index = this.selectedBrands.findIndex(brand => brand.name === item.name);
      this.selectedBrands.splice(index, 1);
    }
  }

  updateSelectedRestaurants() {
    // Clear selected restaurants
    this.selectedRestaurants = [];
    this.selectedRestaurantNames = [];
    // Add selected restaurants from selected brands
    this.selectedBrands.forEach(brand => {
      brand.restaurants.forEach(restaurant => {
        if (restaurant.selected) {
          this.selectedRestaurants.push(restaurant);
          this.selectedRestaurantNames.push(restaurant.name);
        }
      });
    });
  }
}
