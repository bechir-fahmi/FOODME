import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { RegionVM } from 'app/routes/ReferentialData/Models/location-data/RegionVM';
import { RegionService } from 'app/routes/ReferentialData/Services/location-data/region/region.service';
import { CreateOrEditregionModalComponent } from '../create-or-edit-region-modal/create-or-edit-region-modal.component';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.scss']
})
export class RegionComponent implements OnInit {
  displayedColumns = [ 'name', 'country', 'sDMId', 'actions'];
  dataSource: MatTableDataSource<RegionVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private regionservice: RegionService,
    public dialog: MatDialog,private router: Router,private route: ActivatedRoute) {
  }
   regions: RegionVM[] = [];

  ngOnInit() {
    this.getRegions();
  }

  editRegion(region: RegionVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'updateRegion'+id]);
  }

  createRegion()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createRegion");
    // this.router.navigate([navigateToCreate]);
  }

  viewRegion(region: RegionVM)
  {
    const id = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/', 'viewRegion'+id]);
  }

  refresh(): void {
    this.regionservice.getRegions().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getRegions()
  {
    //send request to api and get responce
    this.regionservice.getRegions().subscribe(res =>{
      this.regions = res;
      this.dataSource = new MatTableDataSource(this.regions);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteRegion(id: number){
    this.regionservice.deleteRegion(id).subscribe(
      () => {
        this.refresh();
      });
  }

}




