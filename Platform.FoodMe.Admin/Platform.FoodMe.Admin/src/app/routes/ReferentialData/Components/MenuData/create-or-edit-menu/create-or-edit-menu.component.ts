import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuTemplateVM } from 'app/routes/ReferentialData/Models/MenuData/MenuTemplateVM';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';
import { languageResourceVM } from 'app/routes/ReferentialData/Models/language-data/languageResourceVM';
import { MenuService } from 'app/routes/ReferentialData/Services/MenuData/Menu.service';
import { TemplateMenuService } from 'app/routes/ReferentialData/Services/MenuData/MenuTemplate.service';
import { ConfirmDialogModel } from 'app/routes/monitoringTool/cashier/confirm-dialo/confirm-dialo.component';

@Component({
  selector: 'app-create-or-edit-menu',
  templateUrl: './create-or-edit-menu.component.html',
  styleUrls: ['./create-or-edit-menu.component.scss']
})
export class CreateOrEditMenuComponent implements OnInit {
  menu: MenuVM = new MenuVM();
  title? : string;
  action = 0;
  phoneForm = new FormGroup({});
  view=false;
  templateMenus:MenuTemplateVM[]=[];
  languageResourceVM:languageResourceVM[] = [];
  pageCurrent :  string = "" ;

 constructor(private Menuservice: MenuService,private MenuTemplateservice: TemplateMenuService,private fb: FormBuilder, public dialogref: MatDialogRef<CreateOrEditMenuComponent>,private route: ActivatedRoute
  ,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      this.phoneForm = this.fb.group({});
    }
    
  ngOnInit(): void {

    let object = this.route.snapshot.paramMap.get('my_object');
    let page = this.route.snapshot.paramMap.get('page');
    if (object){
      this.menu = JSON.parse(object);
    }
    if(page){
      this.pageCurrent = page;
    }
    this.MenuTemplateservice.getAllTemplateMenus().subscribe(res=>{
      this.templateMenus = res;
    });
    if(this.pageCurrent == "edit") {
      this.title = 'Edit Menu'
     // this.Menu = this.data;
    }
    else {
      this.title = 'Add Menu'
    }
  }

  save(){
    if(this.pageCurrent) {
      
      this.Menuservice.updateMenu(this.menu).subscribe(
        () => {
          this.router.navigate(["menuManagement/menu"]);
        }
      )
    }
    else {
   
      this.menu.languageResources =  [
        {
          "id": 0,
          "code": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
          "value": this.menu.nameLabelCode,
          "languageKey": 1
        }
      ];
      this.menu.nameLabelCode = "3fa85f64-5717-4562-b3fc-2c963f66afa7"
      this.menu.isDefault = true ;
      this.Menuservice.addMenu(this.menu).subscribe(
        () => {
          this.router.navigate(["menuManagement/menu"]);
        }
      )
    }
  }
  eventTemplateMenu(event: any ){
    this.menu.templateMenuId = event.value; 
  }
 /* confirmOpenDialog(sdmid : number): void {
    const message = `Are you sure you want to do this?`;

    const dialogData = new ConfirmDialogModel("Language Ressources","");

    const dialogRef = this.dialog.open(ConfirmDialoComponent, {
      maxWidth: "400px",
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if(this.result){
       this.listOrder.find((list: { sdmid: number; }) => list.sdmid == sdmid ).orderStatus = 2022;
       this.listOrder.find((list: { sdmid: number; }) => list.sdmid == sdmid ).date_order = new Date();
   /*this.cashierService.updateOrders(this.listOrder.find((list: { sdmid: number; }) => list.sdmid == sdmid )).subscribe(
    (res=>{
      console.log(res);
    })
   )
   this.cashierService.listsOrders =this.listOrder;
      }
    });
*/
}