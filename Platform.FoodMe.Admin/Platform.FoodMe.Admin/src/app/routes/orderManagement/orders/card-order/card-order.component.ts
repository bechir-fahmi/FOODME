import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrderVM } from 'app/routes/ReferentialData/Models/Promotion/OrderVM';

@Component({
  selector: 'app-card-order',
  templateUrl: './card-order.component.html',
  styleUrls: ['./card-order.component.scss']
})
export class CardOrderComponent {
  public order: OrderVM;
  constructor(
    @Inject(MAT_DIALOG_DATA) private _data: any
    ) { 
       //like this you'll get the person name ;)
        console.log(this._data.order); 
        this.order = JSON.parse(this._data.order) ;                 
      }
}
