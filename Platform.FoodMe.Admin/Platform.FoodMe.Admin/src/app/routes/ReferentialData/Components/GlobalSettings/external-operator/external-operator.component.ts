import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ExternalOperatorVM } from 'app/routes/ReferentialData/Models/GlobalSettings/ExternalOperatorVM';

@Component({
  selector: 'app-external-operator',
  templateUrl: './external-operator.component.html',
  styleUrls: ['./external-operator.component.scss']
})
export class ExternalOperatorComponent  implements OnInit {


  displayedColumns = ['name','jobStartTime','jobEndTime','isActive','allowDiscount','actions'];
  dataSource: MatTableDataSource<ExternalOperatorVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute) {
  }
   ExternalOperators: ExternalOperatorVM[] = [];

  ngOnInit() {
    this.getExternalOperators();
  }

  editExternalOperator(ExternalOperator: ExternalOperatorVM)
  {
   // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateExternalOperator/${ExternalOperator.id}`)
    //this.router.navigate([navigateToEdit]);
  }

  createExternalOperator()
  {
   // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createExternalOperator");
   // this.router.navigate([navigateToCreate]);
  }

  viewExternalOperator(ExternalOperator: ExternalOperatorVM)
  {
 //   let navigateToView=this.route.snapshot._routerState.url.concat(`/viewExternalOperator/${ExternalOperator.id}`)
   // this.router.navigate([navigateToView]);
  }

  refresh(): void {
    // this.ExternalOperatorService.getExternalOperators().subscribe(res =>{
    //   this.dataSource.data = res;
    // });
  }
  getExternalOperators()
  {
    this.dataSource = new MatTableDataSource(this.ExternalOperators);

    // this.ExternalOperatorService.getExternalOperators().subscribe(res =>{
    //   this.ExternalOperators = res;
    //   this.dataSource = new MatTableDataSource(this.ExternalOperators);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // });
  }
  deleteExternalOperator(id: number){
    // this.ExternalOperatorService.deleteExternalOperator(id).subscribe(
    //   () => {
    //     this.refresh();
    //   });
  }

}








