import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddressListVM } from 'app/routes/ReferentialData/Models/User Data/addressListVM';
import { AddressListService } from 'app/routes/ReferentialData/Services/UserData/addressList.service';

@Component({
  selector: 'app-create-or-edit-address-list',
  templateUrl: './create-or-edit-address-list.component.html',
  styleUrls: ['./create-or-edit-address-list.component.scss']
})
export class CreateOrEditAddressListComponent implements OnInit{

  AddressList: AddressListVM = new AddressListVM();
  title? : string;
  action = 0;

  constructor(private AddressListservice: AddressListService, public dialogref: MatDialogRef<CreateOrEditAddressListComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){}
    
    
  ngOnInit(): void {
   
    if(this.action == 1) {
      this.title = 'Edit AddressList'
      this.AddressList = this.data;
    }
    else {
      this.title = 'Add AddressList'
    }
  }

  save(){
    if(this.action == 1) {
      this.AddressListservice.updateAddressList(this.AddressList).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      this.AddressListservice.addAddressList(this.AddressList).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }
}

