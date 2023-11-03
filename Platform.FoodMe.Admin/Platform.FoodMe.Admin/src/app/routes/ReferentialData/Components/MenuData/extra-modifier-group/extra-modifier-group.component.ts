import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AdjectiveVM } from 'app/routes/ReferentialData/Models/MenuData/AdjectiveVM';
import { ExtraModifierGroupVM } from 'app/routes/ReferentialData/Models/MenuData/ExtraModifierGroupVM';
import { ExtraModifierGroupService } from 'app/routes/ReferentialData/Services/MenuData/ExtraModifierGroup.service';

@Component({
  selector: 'app-extra-modifier-group',
  templateUrl: './extra-modifier-group.component.html',
  styleUrls: ['./extra-modifier-group.component.scss']
})
export class ExtraModifierGroupComponent  implements OnInit {
  displayedColumns = ['name', 'min', 'max', 'rank', 'actions'];
  dataSource: MatTableDataSource<ExtraModifierGroupVM>;
  isFiltreShowed : boolean = false;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  isActiveSelect :  any[] =[
    {viewValue: "All" ,value: 0},{viewValue: "False" ,value: 1},{viewValue: "True" ,value: 2}];
  constructor(private ExtraModifierGroupservice: ExtraModifierGroupService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   ExtraModifierGroups: ExtraModifierGroupVM[] = [];

  ngOnInit() {
    this.getExtraModifierGroups();
  }




  editExtraModifierGroup(extraModifierGroup: ExtraModifierGroupVM)
  {
    //let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateExtraModifierGroup/${ExtraModifierGroup.id}`)
    //this.router.navigate([navigateToEdit]);
    this.router.navigate(["menuManagement/modifierGroup/createModifierGroup" ,  { my_object: JSON.stringify(extraModifierGroup), page :"edit" }]);
  }

  createExtraModifierGroup()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createModifierGroup");
    // this.router.navigate([navigateToCreate]);
  }

  viewExtraModifierGroup(extraModifierGroup: ExtraModifierGroupVM)
  {
 //   let navigateToView=this.route.snapshot._routerState.url.concat(`/viewExtraModifierGroup/${ExtraModifierGroup.id}`)
   // this.router.navigate([navigateToView]);
   this.router.navigate(["menuManagement/modifierGroup/viewModifierGroup",  { my_object: JSON.stringify(extraModifierGroup) }]);

  }

  refresh(): void {
    // this.ExtraModifierGroupservice.getAllExtraModifierGroups().subscribe(res =>{
    //   this.dataSource.data = res;
    // });
    this.dataSource = new MatTableDataSource(this.ExtraModifierGroups);
  }
  getExtraModifierGroups()
  {
    this.dataSource = new MatTableDataSource(this.ExtraModifierGroups);
    //send request to api and get responce
    this.ExtraModifierGroupservice.getAllExtraModifierGroups().subscribe(res =>{
this.ExtraModifierGroups = res;
    this.dataSource = new MatTableDataSource(this.ExtraModifierGroups);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
   });
  }
  deleteExtraModifierGroup(id: number){
    this.ExtraModifierGroupservice.deleteExtraModifierGroup(id).subscribe(
      () => {
        this.refresh();
      });
  }

  getRankModifyGroup(adjectives:AdjectiveVM[]){
    if(adjectives){
      return adjectives[0].rank
    }
    return 0 ;
  }
  showFiltre(){
    this.isFiltreShowed = !this.isFiltreShowed; 
      }
}




