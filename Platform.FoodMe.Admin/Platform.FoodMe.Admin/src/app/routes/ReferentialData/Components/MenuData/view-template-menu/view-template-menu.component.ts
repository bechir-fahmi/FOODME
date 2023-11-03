import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuTemplateVM } from 'app/routes/ReferentialData/Models/MenuData/MenuTemplateVM';
import { TemplateMenuService } from 'app/routes/ReferentialData/Services/MenuData/MenuTemplate.service';
import { BrandService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/Brand.service';

@Component({
  selector: 'app-view-template-menu',
  templateUrl: './view-template-menu.component.html',
  styleUrls: ['./view-template-menu.component.scss']
})
export class ViewTemplateMenuComponent {
 // menu: MenuVM = new MenuVM();
  menuTemplate: MenuTemplateVM = new MenuTemplateVM();
   constructor( private  route: ActivatedRoute,  private templateMenuService :TemplateMenuService,  private brandService:BrandService){
 
   }
   ngOnInit(): void {
     let object = this.route.snapshot.paramMap.get('my_object');
     if (object){
       this.menuTemplate = JSON.parse(object);
     }
      this.brandService.getBrand(this.menuTemplate.brandId).subscribe(res=>{
   console.log(res);
      })
   }
}
