import { Component, OnInit } from '@angular/core';
import { MtxGridColumn } from '@ng-matero/extensions/grid';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { CustomerService } from 'app/routes/ReferentialData/Services/CustomerData/customer.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})

export class CustomerListComponent implements OnInit {
  list: UserVM[] = [];
  isLoading = true;
  showPaginator = true;
  columnResizable = false;
  multiSelectable = true;
  rowSelectable = true;
  hideRowSelectionCheckbox = false;
  rowHover = false;
  rowStriped = false;
  showToolbar = true;
  columnHideable = true;
  columnSortable = true;
  columnPinnable = true;
  expandable = false;
  expansionTpl = false;


  columns: MtxGridColumn[] = [
    {
      header: 'id',
      field: 'id',
      sortable: true,
      minWidth: 50,
      width: '50px',
    },
    {
      header: 'User name',
      field: 'userName',
      sortable: true,
      disabled: true,
      minWidth: 100,
    },
    {
      header: 'Email',
      field: 'email',
      sortable: true,
      disabled: true,
      minWidth: 120,
      width: '120px',
    },
    
    {
      header: 'Phone number',
      field: 'phoneNumber',
      minWidth: 100,
    },
   

    {
      header: 'actions',
      field: 'actions',
      minWidth: 80,
      width: '80px',
      pinned: 'right',
      class: 'header-action',
      type: 'button',
      buttons: [
        {
          icon: 'menu',
          tooltip: 'menu',
          //click: record => this.menu(record),
        },
      ],
    },
    {
      header: 'operation',
      field: 'operation',
      minWidth: 140,
      width: '140px',
      pinned: 'right',
      type: 'button',
      buttons: [
        {
          type: 'icon',
          icon: 'edit',
          tooltip: 'edit',
        //  click: record => this.edit(record),
        },
        {
          color: 'warn',
          icon: 'delete',
          text: 'delete',
          tooltip: 'delete',
          pop: {
            title: 'confirm_delete',
            closeText: 'close',
            okText: 'ok',
          },

         // click: record => this.delete(record),
        },
      ],
    },
  ];

  constructor(private customerService: CustomerService){

  }
  ngOnInit() {

    this.customerService.getAllCustomer().subscribe(res=>{
          this.list = res;
    });
  }

  changeSort(e: any) {
    console.log(e);
  }

    changeSelect(e: any) {
    console.log(e);
  }


}
