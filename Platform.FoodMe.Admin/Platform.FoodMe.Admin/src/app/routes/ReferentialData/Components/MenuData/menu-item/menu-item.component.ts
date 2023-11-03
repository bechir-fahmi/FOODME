import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuItemVM } from 'app/routes/ReferentialData/Models/MenuData/MenuItemVM';
import { MenuItemService } from 'app/routes/ReferentialData/Services/MenuData/MenuItem.service';
import { CreateOrEditMenuItemComponent } from '../create-or-edit-menu-item/create-or-edit-menu-item.component';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.scss']
})
export class MenuItemComponent implements OnInit {
  displayedColumns = ['menuCategory','name', 'price', 'calories','rank','description','searchkey', 'actions'];
  dataSource: MatTableDataSource<MenuItemVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  isFiltreShowed: boolean = false;
  constructor(private MenuItemservice: MenuItemService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   MenuItems: MenuItemVM[] = [];

  ngOnInit() {
    this.getMenuItems();
  }

  editMenuItem(menuItem: MenuItemVM)
  {
   // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateMenu/${MenuItem.id}`)
   // this.router.navigate([navigateToEdit]);
   this.router.navigate(["menuManagement/menuItem/createMenuItem",  { my_object: JSON.stringify(menuItem) , page :"edit"}]);
  }

  createMenuItem()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createMenuItem");
    // this.router.navigate([navigateToCreate]);
  }

  viewMenuItem(menuItem: MenuItemVM)
  {
   // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenu/${MenuItem.id}`)
   // this.router.navigate([navigateToView]);
   this.router.navigate(["menuManagement/menuItem/viewMenuItem",  { my_object: JSON.stringify(menuItem) }]);
   
  }







  refresh(): void {
    this.MenuItemservice.getAllMenuItems().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getMenuItems()
  {
    //send request to api and get responce
    this.MenuItemservice.getAllMenuItems().subscribe(res =>{
      this.MenuItems = res;
      this.dataSource = new MatTableDataSource(this.MenuItems);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteMenuItem(id: number){
    this.MenuItemservice.deleteMenuItem(id).subscribe(
      () => {
        this.refresh();
      });
  }
  showFiltre(){
    this.isFiltreShowed = !this.isFiltreShowed;
  }
  getLabel(object:any) {
    if (object){
      return object.languageResources[0].value;
    }
}
}



