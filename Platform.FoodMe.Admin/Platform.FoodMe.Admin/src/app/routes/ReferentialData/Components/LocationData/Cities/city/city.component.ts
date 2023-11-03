import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { CityVM } from 'app/routes/ReferentialData/Models/location-data/CityVM';
import { CityService } from 'app/routes/ReferentialData/Services/location-data/city/city.service';
import { CreateOrEditCityModalComponent } from '../create-or-edit-city-modal/create-or-edit-city-modal.component';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent implements OnInit {
  displayedColumns = ['name', 'region','code','countryKey', 'latitude', 'longitude', 'sDMId', 'actions'];
  dataSource: MatTableDataSource<CityVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private cityservice: CityService,
    public dialog: MatDialog,private router: Router,private route: ActivatedRoute) {
  }
   cities: CityVM[] = [];

  ngOnInit() {
    this.getCities();
  }

  editCity(city: CityVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'updateCity'+id]);
  }

  createCity()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createCity");
    // this.router.navigate([navigateToCreate]);
  }

  viewCity(area: CityVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'viewCity'+id]);
  }

  refresh(): void {
    this.cityservice.getCities().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getCities()
  {
    //send request to api and get responce
    this.cityservice.getCities().subscribe(res =>{
      this.cities = res;
      this.dataSource = new MatTableDataSource(this.cities);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteCity(id: number){
    this.cityservice.deleteCity(id).subscribe(
      () => {
        this.refresh();
      });
  }

}
