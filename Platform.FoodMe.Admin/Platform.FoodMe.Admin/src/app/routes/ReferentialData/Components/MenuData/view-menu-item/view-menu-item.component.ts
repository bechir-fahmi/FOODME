import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItemVM } from 'app/routes/ReferentialData/Models/MenuData/MenuItemVM';

@Component({
  selector: 'app-view-menu-item',
  templateUrl: './view-menu-item.component.html',
  styleUrls: ['./view-menu-item.component.scss']
})
export class ViewMenuItemComponent {
  menuItem: MenuItemVM =  new  MenuItemVM();
  constructor( private  route:ActivatedRoute){

  }
  ngOnInit(): void {
  let object = this.route.snapshot.paramMap.get('my_object');
  if (object){
    this.menuItem = JSON.parse(object);
  }
}
}
