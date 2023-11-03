import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MenuItemVM } from 'app/routes/ReferentialData/Models/MenuData/MenuItemVM';
import { DeliveryType } from 'app/routes/ReferentialData/Models/Promotion/DeliveryType';
import { MenuItemService } from 'app/routes/ReferentialData/Services/MenuData/MenuItem.service';

@Component({
  selector: 'app-create-or-edit-menu-item',
  templateUrl: './create-or-edit-menu-item.component.html',
  styleUrls: ['./create-or-edit-menu-item.component.scss']
})
export class CreateOrEditMenuItemComponent implements OnInit {
  MenuItem: MenuItemVM = new MenuItemVM();
  title? : string;
  action = 0;
  phoneForm = new FormGroup({});
  DeliveryTypes:any[]=[];
  url:any;
  msg:string;
  
 

 constructor(private MenuItemservice: MenuItemService,private fb: FormBuilder, public dialogref: MatDialogRef<CreateOrEditMenuItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      this.phoneForm = this.fb.group({});
      const DeliveryTypes = Object.keys(DeliveryType)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: DeliveryType[name as keyof typeof DeliveryType],
          name,
        };
      });
      this.DeliveryTypes=DeliveryTypes;
    }
    
  ngOnInit(): void {

    if(this.action == 1) {
      this.title = 'Edit MenuItem'
      this.MenuItem = this.data;
    }
    else {
      this.title = 'Add MenuItem'
    }
  }
  selectFile(event: any) { 
		if(!event.target.files[0] || event.target.files[0].length == 0) {
			this.msg = 'You must select an image';
			return;
		}
		
		var mimeType = event.target.files[0].type;
		
		if (mimeType.match(/image\/*/) == null) {
			this.msg = "Only images are supported";
			return;
		}
		
		var reader = new FileReader();
		reader.readAsDataURL(event.target.files[0]);
		
		reader.onload = (_event) => {
			this.msg = "";
			this.url = reader.result; 
		}
	}

  save(){
    if(this.action == 1) {
      
      this.MenuItemservice.updateMenuItem(this.MenuItem).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      
      this.MenuItemservice.addMenuItem(this.MenuItem).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }

}


