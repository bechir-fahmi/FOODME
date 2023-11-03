import { Component, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MtxGridColumn } from '@ng-matero/extensions/grid';
import { MenuVM } from 'app/routes/ReferentialData/Models/MenuData/MenuVM';
import { CustomerService } from 'app/routes/ReferentialData/Services/CustomerData/customer.service';
import { map, Observable, startWith } from 'rxjs';
import { AddFundWalletComponent } from './add-fund-wallet/add-fund-wallet.component';

@Component({
  selector: 'app-customer-wallet',
  templateUrl: './customer-wallet.component.html',
  styleUrls: ['./customer-wallet.component.scss']
})
export class CustomerWalletComponent {
  dataSource: MatTableDataSource<MenuVM>;
  displayedColumns = ['name', 'menuTemplate', 'sdmMenuId', 'IsDefault', 'actions'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  myControl = new FormControl<string | any>('');
  options: any[] = [{name: 'Mary'}, {name: 'Shelley'}, {name: 'Igor'}];
  filteredOptions: Observable<any[]>;
  walletSelect: any[] = [

    {value: 'all', viewValue: 'ALL'},
    {value: 'point_to_wallet', viewValue: 'point to wallet'},
    {value: 'order_place', viewValue: 'Order Place'},
  ];
  isLoading = true;
  Menus: MenuVM[] = [];
  list: any[] = [];
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
      header: 'SL',
      field: 'SL',
      sortable: true,
      minWidth: 50,
      width: '50px',
    },
    {
      header: 'TRANSACTION ID',
      field: 'TRANSACTION_ID',
      sortable: true,
      disabled: true,
      minWidth: 120,
      width: '120px',
    },
    {
      header: 'CUSTOMER',
      field: 'CUSTOMER',
      sortable: true,
      disabled: true,
      minWidth: 100,
    },
    {
      header: 'CREDIT',
      field: 'CREDIT',
      minWidth: 100,
    },
    {
      header: 'DEBIT',
      field: 'DEBIT',
      minWidth: 100,
    },
    {
      header: 'BALANCE',
      field: 'BALANCE',
      minWidth: 120,
    },
    {
      header: 'TRANSACTION TYPE',
      field: 'TRANSACTION_TYPE',
      minWidth: 120,
      width: '120px',
    },
    {
      header: 'REFERENCE',
      field: 'REFERENCE',
      minWidth: 180,
    },
    {
      header: 'CREATED AT',
      field: 'CREATED_AT',
      minWidth: 180,
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
  constructor(private customerService: CustomerService, public dialog: MatDialog){

  }
  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.options.slice();
      }),
    );
  }
  displayFn(user: any): string {
    return user && user.name ? user.name : '';
  }

  private _filter(name: string): any[] {
    const filterValue = name.toLowerCase();

    return this.options.filter(option => option.name.toLowerCase().includes(filterValue));
  }

  changeSort(e: any) {
    console.log(e);
  }

    changeSelect(e: any) {
    console.log(e);
  }

  openPopupAddFund(){
    this.dialog.open(AddFundWalletComponent,{
      width:'800px',
      height:'400px'
    });
  }
}
