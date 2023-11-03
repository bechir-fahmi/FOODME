import { Component } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {FormControl, FormGroup} from "@angular/forms";
import {RestaurantEvalutionDto} from "../../../Models/CustomerManagement/RestaurantEvalutionDto";
import {MatTableDataSource} from "@angular/material/table";
import {RestaurantEvalutionService} from "../../../Services/RestaurantEvalutionService/RestaurantEvalution.service";

@Component({
  selector: 'app-restaurant-evaluation',
  templateUrl: './restaurant-evaluation.component.html',
  styleUrls: ['./restaurant-evaluation.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class RestaurantEvaluationComponent {
  public isCollapsed = false;
  showTable = false;
  showInputFields: boolean;
  formValue = new FormGroup({
    unlikereson: new FormControl(''),

  })
  expandedElement: RestaurantEvalutionDto | null;
  expandedElement0: RestaurantEvalutionDto | null;
  columnsToDisplay: string[] =  ['orderId','description','unlikeReasonId'];
  columnsToDisplay0: string[] =  ['restaurantId' ,'userId','orderId','evalution'];


  listRestaurantEvalution: RestaurantEvalutionDto[]=[];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  columnsToDisplayWithExpand0 = [...this.columnsToDisplay0, 'expand'];
  dataSource: MatTableDataSource<RestaurantEvalutionDto> = new MatTableDataSource<RestaurantEvalutionDto>;
  dataSource0: MatTableDataSource<RestaurantEvalutionDto> = new MatTableDataSource<RestaurantEvalutionDto>;
  constructor(public restaurantEvalutionService: RestaurantEvalutionService) {
  }
  ngOnInit(): void {
    this.loadData()

  }

  loadData() {
    this.restaurantEvalutionService.getRestaurantEvalution().subscribe(
      (data: any) => {
        console.log('data', data);
        if (data) {

          this.listRestaurantEvalution = data;

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

