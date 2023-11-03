import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MenuCategoryVM } from 'app/routes/ReferentialData/Models/MenuData/MenuCategoryVM';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';
import { MenuService } from 'app/routes/ReferentialData/Services/MenuData/Menu.service';
import { MenuCategoryService } from 'app/routes/ReferentialData/Services/MenuData/MenuCategory.service';

@Component({
  selector: 'app-create-or-edit-menu-category',
  templateUrl: './create-or-edit-menu-category.component.html',
  styleUrls: ['./create-or-edit-menu-category.component.scss']
})
export class CreateOrEditMenuCategoryComponent implements OnInit {
  MenuCategory: MenuCategoryVM = new MenuCategoryVM();
  title? : string;
  action = 0;
  phoneForm = new FormGroup({});
  menus:MenuVM[]=[];
 

 constructor(private MenuCategoryservice: MenuCategoryService,private menuService: MenuService,private fb: FormBuilder, public dialogref: MatDialogRef<CreateOrEditMenuCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      this.phoneForm = this.fb.group({});
    }
    
  ngOnInit(): void {
 this.menuService.getAllMenus().subscribe(res=>{
this.menus=res;
 })
    if(this.action == 1) {
      this.title = 'Edit MenuCategory'
      this.MenuCategory = this.data;
    }
    else {
      this.title = 'Add MenuCategory'
    }
  }

  save(){
    if(this.action == 1) {
      
      this.MenuCategoryservice.updateMenuCategory(this.MenuCategory).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      
      this.MenuCategoryservice.addMenuCategory(this.MenuCategory).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }

}

