import {AfterViewInit, Component, ElementRef, OnInit, QueryList, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { CountryService } from 'app/routes/ReferentialData/Services/location-data/country/country.service';
import { CountryVM } from 'app/routes/ReferentialData/Models/location-data/CountryVM';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.scss']
})
export class CountryComponent implements OnInit {
  displayedColumns = ['name', 'code', 'countryKey', 'sDMId', 'actions'];
  dataSource: MatTableDataSource<CountryVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private countryservice: CountryService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   countries: CountryVM[] = [];

  ngOnInit() {
    this.getcountries();
  }

  editcountry(country: CountryVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'updateCountry'+id]);
  }

  createcountry()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createCountry");
    // this.router.navigate([navigateToCreate]);
  }

  viewcountry(country: CountryVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'viewCountry'+id]);
  }

  refresh(): void {
    this.countryservice.getcountries().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getcountries()
  {
    this.countryservice.getcountries().subscribe(res =>{
      this.countries = res;
      this.dataSource = new MatTableDataSource(this.countries);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deletecountry(id: number){
    this.countryservice.deletecountry(id).subscribe(
      () => {
        this.refresh();
      });
  }
}






