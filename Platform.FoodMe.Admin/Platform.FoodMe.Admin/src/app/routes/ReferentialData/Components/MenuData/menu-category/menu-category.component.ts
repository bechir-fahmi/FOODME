import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuCategoryVM } from 'app/routes/ReferentialData/Models/MenuData/MenuCategoryVM';
import { MenuCategoryService } from 'app/routes/ReferentialData/Services/MenuData/MenuCategory.service';
import { CreateOrEditMenuCategoryComponent } from '../create-or-edit-menu-category/create-or-edit-menu-category.component';
import { MenuService } from 'app/routes/ReferentialData/Services/MenuData/Menu.service';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';


@Component({
  selector: 'app-menu-category',
  templateUrl: './menu-category.component.html',
  styleUrls: ['./menu-category.component.scss']
})
export class MenuCategoryComponent implements OnInit {
  displayedColumns = ['menu','name', 'description', 'priority', 'rank', 'actions'];
  dataSource: MatTableDataSource<MenuCategoryVM>;
  isFiltreShowed : boolean = false;
  menuVM: MenuVM= new MenuVM();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  isActiveSelect :  any[] =[
    {viewValue: "All" ,value: 0},{viewValue: "False" ,value: 1},{viewValue: "True" ,value: 2}];

  
  constructor(private MenuCategoryservice: MenuCategoryService,private menuService: MenuService,private router: Router,private route: ActivatedRoute,
    public dialog: MatDialog) {
  }
   MenuCategories: MenuCategoryVM[] = [];

  ngOnInit() {
    this.getMenuCategories();
  }




  editMenuCategory(menuCategory: MenuCategoryVM)
  {
  ///  let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateMenuCategory/${MenuCategory.id}`)
  //  this.router.navigate([navigateToEdit]);
  this.router.navigate(["menuManagement/menuCategory/createMenuCategory",  { my_object: JSON.stringify(menuCategory), page: "edit" }]);
  }

  createMenuCategory()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createMenuCategory");
    // this.router.navigate([navigateToCreate]);
    this.router.navigate(["menuManagement/menuCategory/createMenuCategory"]);
  }

  viewMenuCategory(menuCategory: MenuCategoryVM)
  {
    //let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenuCategory/${MenuCategory.id}`)

   // menuCategory/viewMenuCategory

    this.router.navigate(["menuManagement/menuCategory/viewMenuCategory",  { my_object: JSON.stringify(menuCategory) }]);
   // this.router.navigate([navigateToView]);
  }

  refresh(): void {
    this.MenuCategoryservice.getAllMenuCategories().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getMenuCategories()
  {
    //send request to api and get responce
    this.MenuCategoryservice.getAllMenuCategories().subscribe(res =>{
      this.MenuCategories = res;
      this.dataSource = new MatTableDataSource(this.MenuCategories);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteMenuCategory(id: number){
    this.MenuCategoryservice.deleteMenuCategory(id).subscribe(
      () => {
        this.refresh();
      });
  }
  showFiltre(){
    this.isFiltreShowed = !this.isFiltreShowed; 
      }

      getLabel(menuCategory:any){

       this.menuService.getMenu(menuCategory.menuId).subscribe(res=>{

       })
      }


      getDescription(value: any){
        if (value){
          return value[0].value;
        }
        
      }
}



