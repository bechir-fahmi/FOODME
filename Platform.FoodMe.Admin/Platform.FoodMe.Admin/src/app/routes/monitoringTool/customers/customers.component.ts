import { Component, OnInit } from '@angular/core';
import { CashierService } from 'app/routes/ReferentialData/Services/monitoringToolsData/cashier.service';

import * as _ from "lodash";// var myUser: any = require('../assets/orders.json');
@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit{
  listOrder: any;
nowDate = new Date();
listeKeyValuesOrdes: any ; 
constructor( private cashierService: CashierService){
  setInterval(() => {
    this.nowDate = new Date()
  }, 1000)
}
  ngOnInit(): void {
   // this.cashierService.getAllOrder().subscribe(res=>{
    this.listOrder = this.cashierService.listsOrders;
      this.listeKeyValuesOrdes = Object.entries(_.groupBy(this.listOrder, 'deliv_Comp')).map(([key, values]) => ({key, values}));

//console.log("mu user"+ myUser)
/*from(this.listsOrders).pipe(
  groupBy(order => order.deliv_Comp),
  mergeMap(group => group
    .pipe(
      reduce(function (acc, cur) {
        acc.values.push(cur);
        return acc;
      },
        { key: group.key, values: [] }
      )
    )
  ),
  toArray()
)*/



  }

  getListOrderValues(index: any ):any[]| null {
console.log(this.listeKeyValuesOrdes[index]);
 // if( this.listeKeyValuesOrdes[index])
 return this.listeKeyValuesOrdes[index].values }
}
