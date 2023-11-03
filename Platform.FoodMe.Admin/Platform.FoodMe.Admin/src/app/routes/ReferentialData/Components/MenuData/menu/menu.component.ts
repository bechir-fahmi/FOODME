
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuTemplateVM } from 'app/routes/ReferentialData/Models/MenuData/MenuTemplateVM';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';
import { MenuService } from 'app/routes/ReferentialData/Services/MenuData/Menu.service';
import { TemplateMenuService } from 'app/routes/ReferentialData/Services/MenuData/MenuTemplate.service';
import { template } from 'devextreme/core/templates/template';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  displayedColumns = ['name', 'menuTemplate', 'sdmMenuId', 'IsDefault', 'actions'];
  dataSource: MatTableDataSource<MenuVM>;
  isFiltreShowed : boolean = false;
  templateMenu:MenuTemplateVM = new MenuTemplateVM();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private Menuservice: MenuService,private router: Router,private route: ActivatedRoute,private templateMenuService:TemplateMenuService,
    public dialog: MatDialog) {
  }
   Menus: MenuVM[] = [];

  ngOnInit() {
    this.getMenus();
  }

  editMenu(menu: MenuVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateMenu/${Menu.id}`)
    // this.router.navigate([navigateToEdit]);

  }

  createMenu()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createMenu");
     this.router.navigate(["menuManagement/menu/createMenu"]);
  }

  viewMenu(menu: MenuVM)
  {
  //  let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenu/${Menu.id}`)
   // this.router.navigate([navigateToView]);
   this.router.navigate(["menuManagement/menu/viewMenu",  { my_object: JSON.stringify(menu) }]);
  }

  refresh(): void {
    this.Menuservice.getAllMenus().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getMenus()
  {
    //send request to api and get responce
    this.Menuservice.getAllMenus().subscribe(res =>{
      this.Menus = res;
      this.dataSource = new MatTableDataSource(this.Menus);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteMenu(id: number){
    this.Menuservice.deleteMenu(id).subscribe(
      () => {
        this.refresh();
      });
  }
  showFiltre(){
    this.isFiltreShowed = !this.isFiltreShowed; 
      }
getLabelTemplateMenu(menu:MenuVM){
  this.templateMenuService.getTemplateMenu(menu.templateMenuId).subscribe(res=>{
 this.templateMenu = res;
       })
}
getLabel(object:any) {
  if (object){
    return object.languageResources[0].value;
  }

}
 
}

