import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuTemplateVM } from 'app/routes/ReferentialData/Models/MenuData/MenuTemplateVM';
import { TemplateMenuService } from 'app/routes/ReferentialData/Services/MenuData/MenuTemplate.service';
import { CreateOrEditTemplateMenuComponent } from '../create-or-edit-template-menu/create-or-edit-template-menu.component';

@Component({
  selector: 'app-template-menu',
  templateUrl: './template-menu.component.html',
  styleUrls: ['./template-menu.component.scss']
})
export class TemplateMenuComponent implements OnInit {
  displayedColumns = ['name','brand', 'sdmMenuTemplateId', 'actions'];
  dataSource: MatTableDataSource<MenuTemplateVM>;
  isFiltreShowed : boolean = false;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private TemplateMenuservice: TemplateMenuService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   TemplateMenus: MenuTemplateVM[] = [];

  ngOnInit() {
    this.getTemplateMenus();
  }

  editTemplateMenu(menuTemplate: MenuTemplateVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateMenu/${MenuTemplate.id}`)
    // this.router.navigate([navigateToEdit]);

  }

  createTemplateMenu()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createMenuTemplate");
    // this.router.navigate([navigateToCreate]);
  }

  viewTemplateMenu(menuTemplate: MenuTemplateVM)
  {
    //let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenu/${MenuTemplate.id}`)
   // this.router.navigate([navigateToView]);
   this.router.navigate(["menuManagement/menuTemplate/viewMenuTemplate", {my_object: JSON.stringify(menuTemplate)}]);

 
  }

  refresh(): void {
    this.TemplateMenuservice.getAllTemplateMenus().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getTemplateMenus()
  {
    //send request to api and get responce
    this.TemplateMenuservice.getAllTemplateMenus().subscribe(res =>{
      this.TemplateMenus = res;
      this.dataSource = new MatTableDataSource(this.TemplateMenus);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteTemplateMenu(id: number){
    this.TemplateMenuservice.deleteTemplateMenu(id).subscribe(
      () => {
        this.refresh();
      });
  }
  getLabel(object :  any ){
    if (object){
      return object.nameLabelCode;
    }  

  }


  showFiltre(){
    this.isFiltreShowed = !this.isFiltreShowed; 
      }
}


