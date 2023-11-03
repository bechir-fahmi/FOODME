import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuCategoryVM } from 'app/routes/ReferentialData/Models/MenuData/MenuCategoryVM';

@Component({
  selector: 'app-view-menu-category',
  templateUrl: './view-menu-category.component.html',
  styleUrls: ['./view-menu-category.component.scss']
})
export class ViewMenuCategoryComponent {
 menuCategorey: MenuCategoryVM =  new  MenuCategoryVM()
  constructor( private  route:ActivatedRoute){

  }
  ngOnInit(): void {
  let object = this.route.snapshot.paramMap.get('my_object');
  if (object){
    this.menuCategorey = JSON.parse(object);
  }
}
}
