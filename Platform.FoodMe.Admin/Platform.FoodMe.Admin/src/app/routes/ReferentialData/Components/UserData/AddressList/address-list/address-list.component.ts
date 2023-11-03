import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AddressListVM } from 'app/routes/ReferentialData/Models/User Data/addressListVM';
import { AddressListService } from 'app/routes/ReferentialData/Services/UserData/addressList.service';
import { CreateOrEditAddressListComponent } from '../create-or-edit-address-list/create-or-edit-address-list.component';

@Component({
  selector: 'app-address-list',
  templateUrl: './address-list.component.html',
  styleUrls: ['./address-list.component.scss']
})
export class AddressListComponent implements OnInit {
  displayedColumns = ['nameLabelCode', 'actions'];
  dataSource: MatTableDataSource<AddressListVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private AddressListservice: AddressListService,
    public dialog: MatDialog) {
  }
  AddressLists: AddressListVM[] = [];

  ngOnInit() {
    this.getAddressLists();
  }
  editAddressList(AddressList: AddressListVM)
  {
    let dialogRef = this.dialog.open(CreateOrEditAddressListComponent, {
      data: AddressList
    });
    dialogRef.componentInstance.action = 1;
   
  }

  createAddressList()
  {
    this.dialog.afterAllClosed.subscribe(()=> this.refresh());
    this.dialog.open(CreateOrEditAddressListComponent);
   
  }

  viewAddressList(AddressList: AddressListVM)
  {
    this.dialog.open(CreateOrEditAddressListComponent, {
      data: AddressList
    });
  
  }

  refresh(): void {
    this.AddressListservice.getAllAddressLists().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getAddressLists()
  {
    //send request to api and get responce
    this.AddressListservice.getAllAddressLists().subscribe(res =>{
      this.AddressLists = res;
      this.dataSource = new MatTableDataSource(this.AddressLists);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteAddressList(id: number){
    this.AddressListservice.deleteAddressList(id).subscribe(
      () => {
        this.refresh();
      });
  }
}



