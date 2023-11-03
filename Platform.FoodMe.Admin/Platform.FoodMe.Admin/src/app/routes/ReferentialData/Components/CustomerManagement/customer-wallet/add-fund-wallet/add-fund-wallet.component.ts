import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-fund-wallet',
  templateUrl: './add-fund-wallet.component.html',
  styleUrls: ['./add-fund-wallet.component.scss']
})
export class AddFundWalletComponent  implements OnInit {
  title? : string;
  options: any[] = [{name: 'Mary'}, {name: 'Shelley'}, {name: 'Igor'}];
  myControl = new FormControl<string | any>('');
  filteredOptions: Observable<any[]>;
  ngOnInit(): void {
    this.title = "Add Fund"
  }
  save(){

  }
  displayFn(user: any): string {
    return user && user.name ? user.name : '';
  }
}
