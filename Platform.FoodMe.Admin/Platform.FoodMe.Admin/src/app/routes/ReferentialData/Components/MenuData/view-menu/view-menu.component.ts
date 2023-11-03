import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuTemplateVM } from 'app/routes/ReferentialData/Models/MenuData/MenuTemplateVM';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';
import { TemplateMenuService } from 'app/routes/ReferentialData/Services/MenuData/MenuTemplate.service';

@Component({
  selector: 'app-view-menu',
  templateUrl: './view-menu.component.html',
  styleUrls: ['./view-menu.component.scss']
})
export class ViewMenuComponent implements OnInit {

 menu: MenuVM = new MenuVM();
 menuTemplate: MenuTemplateVM = new MenuTemplateVM();
  constructor( private  route:ActivatedRoute,  private templateMenuService:TemplateMenuService){

  }
  ngOnInit(): void {
    let object = this.route.snapshot.paramMap.get('my_object');
    if (object){
      this.menu = JSON.parse(object);
    }
     this.templateMenuService.getTemplateMenu(this.menu.templateMenuId).subscribe(res=>{
  this.menuTemplate = res;
     })
  }
}
