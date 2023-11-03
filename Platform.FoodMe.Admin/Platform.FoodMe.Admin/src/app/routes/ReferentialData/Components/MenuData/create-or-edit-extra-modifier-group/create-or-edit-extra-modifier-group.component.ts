import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ExtraModifierGroupVM } from 'app/routes/ReferentialData/Models/MenuData/ExtraModifierGroupVM';
import { ExtraModifierGroupService } from 'app/routes/ReferentialData/Services/MenuData/ExtraModifierGroup.service';

@Component({
  selector: 'app-create-or-edit-extra-modifier-group',
  templateUrl: './create-or-edit-extra-modifier-group.component.html',
  styleUrls: ['./create-or-edit-extra-modifier-group.component.scss']
})
export class CreateOrEditExtraModifierGroupComponent implements OnInit {
  ExtraModifierGroup: ExtraModifierGroupVM = new ExtraModifierGroupVM();
  title? : string;
  view=false;


  constructor(private ExtraModifierGroupservice: ExtraModifierGroupService, public dialogref: MatDialogRef<CreateOrEditExtraModifierGroupComponent>,private route: ActivatedRoute,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    
  ngOnInit(): void {


    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
   {
    this.title = 'Edit ExtraModifierGroup'
     this.ExtraModifierGroupservice.getExtraModifierGroup(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.ExtraModifierGroup=res});
  }
  if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
  {
    this.view=true;
   this.title = 'View ExtraModifierGroup'
    this.ExtraModifierGroupservice.getExtraModifierGroup(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.ExtraModifierGroup=res});
 }
 if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
      this.title = 'Add ExtraModifierGroup'
    }
  }

  save(){
    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update")) {
      
      this.ExtraModifierGroupservice.updateExtraModifierGroup(this.ExtraModifierGroup).subscribe(
        () => {
          this.router.navigate(["/ExtraModifierGroupManagement/ExtraModifierGroups"]);
        }
      )
    }
    else {
      
      this.ExtraModifierGroup.languageResources =  [
        {
          "id": 0,
          "code": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
          "value": this.ExtraModifierGroup.nameLabelCode,
          "languageKey": 1
        }
      ];
      this.ExtraModifierGroup.nameLabelCode = "3fa85f64-5717-4562-b3fc-2c963f66afa7"
 
      this.ExtraModifierGroupservice.addExtraModifierGroup(this.ExtraModifierGroup).subscribe(
        () => {
          this.router.navigate(["menuManagement/modifierGroup"]);
        }
      )
    }
  }

}


