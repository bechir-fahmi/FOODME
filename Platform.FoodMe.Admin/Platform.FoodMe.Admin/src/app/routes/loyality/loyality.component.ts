import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoyalityService } from './loyality.service';
import { TreeItemDragEvent } from '@progress/kendo-angular-treeview';

@Component({
  selector: 'app-loyality',
  templateUrl: './loyality.component.html',
  styleUrls: ['./loyality.component.scss']
})
export class LoyalityComponent implements OnInit{
  loyalityForm: FormGroup;
  public expandedKeys: number[] = [2];
  public listViewData: any[] = [];
  public employees: any[] = [
    { employeeId: 2, name: 'Andrew Fuller', reportsTo: null },
    { employeeId: 1, name: 'Nancy Davolio', reportsTo: 2 },
    { employeeId: 3, name: 'Janet Leverling', reportsTo: 2 },
    { employeeId: 4, name: 'Margaret Peacock', reportsTo: 2 },
    { employeeId: 5, name: 'Steven Buchanan', reportsTo: 2 },
    { employeeId: 8, name: 'Laura Callahan', reportsTo: 2 },
    { employeeId: 6, name: 'Michael Suyama', reportsTo: 5 },
    { employeeId: 7, name: 'Robert King', reportsTo: 5 },
    { employeeId: 9, name: 'Anne Dodsworth', reportsTo: 5 }
  ];

  constructor(
    private formBuilder: FormBuilder,
    private loyalityService: LoyalityService
  ) {}



  ngOnInit(): void {
    this.loyalityForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      CashBackMode: ['', [Validators.required]],
      mandatorySubscription: ['', [Validators.required]],
      status: [''],
    });
  }

  addLoyaltySetting(){
    const loyality: any = {
      isActiveDownloadAppPoints: this.loyalityForm.value.isActiveDownloadAppPoints,
      downloadAppPoints: this.loyalityForm.value.downloadAppPoints,
      isActiveFirstTimeOrderPoints:this.loyalityForm.value.isActiveFirstTimeOrderPoints,
      firstTimeOrderPoints: this.loyalityForm.value.firstTimeOrderPoints,
      isActiveNewOrderTotalAmountPoints:this.loyalityForm.value.isActiveNewOrderTotalAmountPoints,
      isActivePurchasePoints: this.loyalityForm.value.isActivePurchasePoints,
      equivalencePointUnit: this.loyalityForm.value.equivalencePointUnit,
    };
    this.loyalityService.addLoyaltySetting(loyality).subscribe(
      response => console.log(response),
      error => console.log(error)
    );
  }


  //Drag and Drop
  onNodeDrag(event: TreeItemDragEvent) {
    console.log(event);
    const children = event.sourceItem?.children;
    if (children && children.length > 0) {
      children.forEach(c =>
        this.listViewData.push(c.item.dataItem)
      );
    } else {
      this.listViewData.push(event.sourceItem.item.dataItem);
    }

    this.listViewData = [...this.listViewData];
  }

}
