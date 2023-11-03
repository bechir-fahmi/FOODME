import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AreaVM } from 'app/routes/ReferentialData/Models/location-data/AreaVM';
import { AreaService } from 'app/routes/ReferentialData/Services/location-data/area/area.service';
import { CreateOrEditAreaModalComponent } from '../create-or-edit-area-modal/create-or-edit-area-modal.component';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss']
})
export class AreaComponent implements OnInit {
  displayedColumns = ['name', 'city','code', 'latitude', 'longitude', 'sDMId', 'actions'];
  dataSource: MatTableDataSource<AreaVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private areaservice: AreaService,
    public dialog: MatDialog,private router: Router,private route: ActivatedRoute) {
  }
   areas: AreaVM[] = [];

  ngOnInit() {
    this.getAreas();
  }

  editArea(area: AreaVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'updateArea'+id]);
  }

  createArea()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createArea");
    // this.router.navigate([navigateToCreate]);
  }

  viewArea(area: AreaVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'viewCountry'+id]);
  }

  refresh(): void {
    this.areaservice.getAreas().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getAreas()
  {
    //send request to api and get responce
   /* this.areaservice.getAreas().subscribe(res =>{
      this.areas = res;
      this.dataSource = new MatTableDataSource(this.areas);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });*/
    this.router.navigate(["referentialData/locationData/areas/"]);
  }
  deleteArea(id: number){
    this.areaservice.deleteArea(id).subscribe(
      () => {
        this.refresh();
      });
  }

}
