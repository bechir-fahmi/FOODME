import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuTemplateVM } from 'app/routes/ReferentialData/Models/MenuData/MenuTemplateVM';
import { BrandVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { TemplateMenuService } from 'app/routes/ReferentialData/Services/MenuData/MenuTemplate.service';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';

@Component({
  selector: 'app-create-or-edit-template-menu',
  templateUrl: './create-or-edit-template-menu.component.html',
  styleUrls: ['./create-or-edit-template-menu.component.scss']
})
export class CreateOrEditTemplateMenuComponent implements OnInit {
  TemplateMenu: MenuTemplateVM = new MenuTemplateVM();
  title? : string;
  action = 0;
  phoneForm = new FormGroup({});
  page :string;
 brands:BrandVM[]=[];

 constructor(private TemplateMenuservice: TemplateMenuService,private Brandservice: BrandService,private route: ActivatedRoute,private fb: FormBuilder,private router: Router, public dialogref: MatDialogRef<CreateOrEditTemplateMenuComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      this.phoneForm = this.fb.group({});
    }
    
  ngOnInit(): void {
    let object = this.route.snapshot.paramMap.get('my_object');
    let page = this.route.snapshot.paramMap.get('page');
    if (object){
      this.TemplateMenu = JSON.parse(object);
    }
    if(page){
      this.page = page;
    }

 this.Brandservice.getAllBrands().subscribe(res=>{this.brands=res})
    if(this.page == "edit") {
      this.title = 'Edit TemplateMenu'
     // this.TemplateMenu = this.data;
    }
    else {
      this.title = 'Add TemplateMenu'
    }
  }

  save(){
    if(this.page == "edit") {
      
      this.TemplateMenuservice.updateTemplateMenu(this.TemplateMenu).subscribe(
        () => {
          this.router.navigate(["menuManagement/menuTemplate"]);
        }
      )
    }
    else {

      this.TemplateMenuservice.addTemplateMenu(this.TemplateMenu).subscribe(
        () => {
          this.router.navigate(["menuManagement/menuTemplate"]);
        }
      )
    }
  }

}
