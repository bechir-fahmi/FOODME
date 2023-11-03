import { Component } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {FormControl, FormGroup} from "@angular/forms";
import {MatTableDataSource} from "@angular/material/table";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {OrderActionReasonDto} from "../../../Models/OrderManagement/OrderActionReasonDto";
import {OrderRefundsReasonDto} from "../../../Models/OrderManagement/OrderRefundsReasonDto";
import {OrderActionReasonService} from "../../../Services/OrderManagement/OrderActionReason.service";
import {RestaurantEvalutionDto} from "../../../Models/CustomerManagement/RestaurantEvalutionDto";
import {RestaurantEvalutionService} from "../../../Services/RestaurantEvalutionService/RestaurantEvalution.service";
import {DriverEvalutionDto} from "../../../Models/CustomerManagement/DriverEvalutionDto";
import {DriverEvalutionService} from "../../../Services/DriverEvalutionService/DriverEvalution.service";
@Component({
  selector: 'app-driver-evaluation',
  templateUrl: './driver-evaluation.component.html',
  styleUrls: ['./driver-evaluation.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DriverEvaluationComponent {
  public isCollapsed = false;
  showTable = false;
  showInputFields: boolean;
  formValue = new FormGroup({
    unlikereson: new FormControl(''),

  })
  expandedElement: DriverEvalutionDto | null;
  expandedElement0: DriverEvalutionDto | null;
  columnsToDisplay: string[] =  ['orderId','description','unlikeReasonId'];
  columnsToDisplay0: string[] =  ['driverName' ,'driverPhone','driverCompany','userId','orderId','evalution'];


  listDriverEvalution: DriverEvalutionDto[]=[];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  columnsToDisplayWithExpand0 = [...this.columnsToDisplay0, 'expand'];
  dataSource: MatTableDataSource<DriverEvalutionDto> = new MatTableDataSource<DriverEvalutionDto>;
  dataSource0: MatTableDataSource<DriverEvalutionDto> = new MatTableDataSource<DriverEvalutionDto>;
  constructor(public driverEvalutionService: DriverEvalutionService) {
  }
  ngOnInit(): void {
    this.loadData()

  }

  loadData() {
    this.driverEvalutionService.getDriverEvalution().subscribe(
      (data: any) => {
        console.log('data', data);
        if (data) {

          this.listDriverEvalution = data;

        }
      }
    )
  }


  getStarRating(rating: number): string {
    let stars = '';
    for (let i = 0; i < rating; i++) {
      stars += '★';
    }
    for (let i = rating; i < 5; i++) {
      stars += '☆';
    }
    return stars;
  }

  addColumn() {

    this.columnsToDisplay.push('newColumn');

  }

  createreason() {

  }

  exportExcel() {

  }
}

