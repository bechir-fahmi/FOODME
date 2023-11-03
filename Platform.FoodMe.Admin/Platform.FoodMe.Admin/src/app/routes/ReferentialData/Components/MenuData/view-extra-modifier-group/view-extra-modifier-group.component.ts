import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { AdjectiveVM } from 'app/routes/ReferentialData/Models/MenuData/AdjectiveVM';
import { ExtraModifierGroupVM } from 'app/routes/ReferentialData/Models/MenuData/ExtraModifierGroupVM';
import { MenuElementVM } from 'app/routes/ReferentialData/Models/MenuData/MenuElementVM';

@Component({
  selector: 'app-view-extra-modifier-group',
  templateUrl: './view-extra-modifier-group.component.html',
  styleUrls: ['./view-extra-modifier-group.component.scss']
})
export class ViewExtraModifierGroupComponent  implements OnInit {
//  ExtraModifierGroupVM
displayedColumns: string[] = ['ItemName', 'Price'];

dataSource: MatTableDataSource<MenuElementVM>;
extraModifierGroup: ExtraModifierGroupVM = new ExtraModifierGroupVM();
//menuTemplate: MenuTemplateVM = new MenuTemplateVM();
@ViewChild(MatPaginator) paginator: MatPaginator;
@ViewChild(MatSort) sort: MatSort;
 constructor( private  route:ActivatedRoute){

 }
 ngOnInit(): void {
   let object = this.route.snapshot.paramMap.get('my_object');
   if (object){
     this.extraModifierGroup = JSON.parse(object);

     this.dataSource = new MatTableDataSource( this.extraModifierGroup.element);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.sort;
   }
   /* this.templateMenuService.getTemplateMenu(this.menu.templateMenuId).subscribe(res=>{
 this.menuTemplate = res;
    })*/
 }

 getRankModifyGroup(adjectives:AdjectiveVM[]){
  if(adjectives){
    return adjectives[0].rank
  }
  return 0 ;
}

getVisibilityModifyGroup(adjectives:AdjectiveVM[]){
  if(adjectives){
    return adjectives[0].visibility
  }
  return 0 ;
}
}
