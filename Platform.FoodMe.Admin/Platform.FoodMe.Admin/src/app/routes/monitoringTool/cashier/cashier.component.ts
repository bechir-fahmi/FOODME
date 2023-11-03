import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { OrderVM } from 'app/routes/ReferentialData/Models/Promotion/OrderVM';
import { CashierService } from 'app/routes/ReferentialData/Services/monitoringToolsData/cashier.service';
import { debounce, interval, Observable, of, switchMap, tap } from 'rxjs';
import { ConfirmDialoComponent, ConfirmDialogModel } from './confirm-dialo/confirm-dialo.component';
@Component({
  selector: 'app-cashier',
  templateUrl: './cashier.component.html',

  styleUrls: ['./cashier.component.scss'],
 
  
})
export class CashierComponent implements OnInit{
  isFiltreShowed : boolean = false;
  listOrder: any;
  result: string = '';
  values = '';
  searchText:any;
  searchRestauText:any;
  result_list:any;
  item:string  = '';

search_word = new FormControl();
isLoading = false;
filteredItems:any;
  constructor(private cashierService: CashierService,
    public dialog: MatDialog){


  }
  ngOnInit(): void {
    this.cashierService.getAllOrder().subscribe(res=>{
     
});
this.listOrder = this.cashierService.listsOrders;
   /* this.search_word.valueChanges.pipe(
      tap(() => this.isLoading = true),
      debounce(() => interval(1000)),
      switchMap(value => this.search(value))
    ).subscribe(res => {
      this.listsOrders = res;
      this.isLoading = false;
    },
    err => {
      console.error(err.error);
    });*/



  }
//  CashierService
  showFiltre(){
this.isFiltreShowed = !this.isFiltreShowed; 
  }

  confirmDialog(sdmid : number): void {
    const message = `Are you sure you want to do this?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

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
   )*/
   this.cashierService.listsOrders =this.listOrder;
      }
    });
  }


  

  onKey(event: any) { // without type info
    this.values += event.target.value + ' | ';
  /* this.listsOrders = this.listsOrders.pipe(
    filter(
      (character: StarWarsCharacter) => {
         character.gender.toLowerCase() === value.toLowerCase()
      }
    ),
 );*/
  }

  search(keyword: string): Observable<any> {
    console.log(keyword);
    const result = this.listOrder.filter((e: string | string[]) => e.indexOf(keyword) !== -1)
    return of(result)
  }
  assignCopy(){
    this.filteredItems = Object.assign([], this.listOrder);
 }
 
  filterItem(value: string){
    if(!value){
        this.assignCopy();
    } // when nothing has typed

 }
 changeValues( event :any ){
  this.item = "company";
  this.searchText = event.target.value;
 }
 changeValuesRestau( event :any ){
  this.item = "restaurant";
  this.searchText = event.target.value;
 }
 changeValuesnumber( event :any ){
  this.item = "number";
  this.searchText = event.target.value;
 }
}
