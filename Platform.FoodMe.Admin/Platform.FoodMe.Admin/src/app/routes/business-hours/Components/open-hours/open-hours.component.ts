import { Component, OnInit } from '@angular/core';
import {
  WorkingTime,
  UsualDailyWorkingTime,
  ExceptionalDailyWorkingTime,
} from './../../Models/WorkingTime';
import { BusinessHoursService } from '../../business-hours.service';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import * as moment from 'moment';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Brand, Restaurant } from '../../Models/Brand';

@Component({
  selector: 'app-open-hours',
  templateUrl: './open-hours.component.html',
  styleUrls: ['./open-hours.component.css'],
})
export class OpenHoursComponent implements OnInit {
  workingTimeForm: FormGroup;
  workingTimeForm2: FormGroup;
  dayOfWeek = new FormControl(0);
  defaultTime = '12:00:00';
  defaultDay = 'Monday';

  // workHoursForm: any;
  public daysTime: any;
  daysOfWeek = [
    { id: 1, name: 'Monday' },
    { id: 2, name: 'Tuesday' },
    { id: 3, name: 'Wednesday' },
    { id: 4, name: 'Thursday' },
    { id: 5, name: 'Friday' },
    { id: 6, name: 'Saturday' },
    { id: 7, name: 'Sunday' },
  ];

  times = [
    '12:00:00',
    '12:30:00',
    '1:00:00',
    '1:30:00',
    '2:00:00',
    '2:30:00',
    '3:00:00',
    '3:30:00',
    '4:00:00',
    '4:30:00',
    '5:00:00',
    '5:30:00',
    '6:00:00',
    '6:30:00',
    '7:00:00',
    '7:30:00',
    '8:00:00',
    '8:30:00',
    '9:00:00',
    '9:30:00',
    '10:00:00',
    '10:30:00',
    '11:00:00',
    '11:30:00',
    '12:00:00',
  ];

  afternoonTimes = [
    '12:00:00',
    '12:30:00',
    '13:00:00',
    '13:30:00',
    '14:00:00',
    '14:30:00',
    '15:00:00',
    '15:30:00',
    '16:00:00',
    '16:30:00',
    '17:00:00',
    '17:30:00',
    '18:00:00',
    '18:30:00',
    '19:00:00',
    '19:30:00',
    '20:00:00',
    '20:30:00',
    '21:00:00',
    '21:30:00',
    '22:00:00',
    '22:30:00',
    '23:00:00',
    '23:30:00',
    '00:00:00',
  ];

  exceptionalDailyWorkingTimes: ExceptionalDailyWorkingTime[] = [
    {
      id: 0,
      dateTime: '2023-04-19T09:46:40.733Z',
      nameLabelCode: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
      descriptionLabelCode: '3fa85f64-5717-4562-b3fc-2c963f66afa6',

    },
  ];

  constructor(
    private businessHoursService: BusinessHoursService,
    private fb: FormBuilder,
    private activatedroute: ActivatedRoute,
    private location: Location
  ) {
  }

  ngOnInit(): void {
    this.createForm();
    this.additionalHour().push(this.newAdditional());
  }


  createForm() {
    this.workingTimeForm = this.fb.group({
      usualDailyWorkingTimes: this.fb.array([]),
    });

    this.workingTimeForm2 = this.fb.group({
      additionalHour: this.fb.array([]),
    });
    // Get the form array
    const usualDailyWorkingTimes = this.workingTimeForm.get('usualDailyWorkingTimes') as FormArray;

    // Loop through the daysList array and create a form group for each day
    this.daysOfWeek.forEach(day => {
      const usualDailyWorkingTime = this.fb.group({
        day:[day.id],
        morningStartTime:  ['', [Validators.required]],
        morningCloseTime: ['', [Validators.required]],
        afterNoonStartTime: ['', [Validators.required]],
        afterNoonCloseTime: ['', [Validators.required]],
        isClosed: [false],
      });

      // Add the form group to the form array
      usualDailyWorkingTimes.push(usualDailyWorkingTime);
    });
  }

  additionalHour(): FormArray {
    return this.workingTimeForm2.get('additionalHour') as FormArray;
  }

  newAdditional(): FormGroup {
    return this.fb.group({
      dateTime: '',
      prdescriptionLabelCodeice: '',
    });
  }

  addAdditional() {
    this.additionalHour().push(this.newAdditional());
  }

  removeAdditional(i: number) {
    this.additionalHour().removeAt(i);
  }
  get usualDailyWorkingTimes(): FormArray {
    return this.workingTimeForm.get('usualDailyWorkingTimes') as FormArray;
  }

  toggleTimeDropdowns(event: Event, formControls: { [key: string]: FormControl }) {
    const isClosed = (event.target as HTMLInputElement).checked;
    if (isClosed) {
      formControls.morningStartTime?.disable();
      formControls.morningCloseTime?.disable();
      formControls.afterNoonStartTime?.disable();
      formControls.afterNoonCloseTime?.disable();
    } else {
      formControls.morningStartTime?.enable();
      formControls.morningCloseTime?.enable();
      formControls.afterNoonStartTime?.enable();
      formControls.afterNoonCloseTime?.enable();
    }
  }

  onSubmit() {
    const workingTime: any = {
      restaurantId: Number(this.activatedroute.snapshot.paramMap.get('id')),
      usualDailyWorkingTimes: this.workingTimeForm.value.usualDailyWorkingTimes,
      exceptionalDailyWorkingTimes: this.exceptionalDailyWorkingTimes,
    };
    this.businessHoursService.addWorkingTime(workingTime).subscribe(
      response => {
        this.cancel();
        console.log(response);},
      error => console.log(error)
    );
  }

  cancel(){
    this.location.back();

  }

  //filter
  selectedItems = [];
  selectedBrands:Brand[]=[];
   brands: Brand[] = [
    {
      name: 'Brand1',
      selected: false,
      restaurants: [
        { name: 'Restaurant1', selected: false },
        { name: 'Restaurant2', selected: false },
        { name: 'Restaurant3', selected: false }
      ]
    },
    {
      name: 'Brand2',
      selected: false,
      restaurants: [
        { name: 'Restaurant4', selected: false },
        { name: 'Restaurant5', selected: false },
        { name: 'Restaurant6', selected: false }
      ]
    },
    {
      name: 'Brand3',
      selected: false,
      restaurants: [
        { name: 'Restaurant7', selected: false },
        { name: 'Restaurant8', selected: false },
        { name: 'Restaurant9', selected: false }
      ]
    }
  ];

  isSelected(item:any): boolean {
    // return this.selectedItems.indexOf() > -1;
    return true;
  }

  onSelect(): void {
    console.log(this.selectedItems);
  }

  selectedRestaurants: Restaurant[] = [];
  selectedRestaurantNames: string[] = [];

  updateSelectedBrands(item: Brand, isChecked: boolean) {
    if (isChecked) {
      this.selectedBrands.push(item);
    } else {
      const index = this.selectedBrands.findIndex((brand) => brand.name === item.name);
      this.selectedBrands.splice(index, 1);
    }
  }

  updateSelectedRestaurants() {
    // Clear selected restaurants
    this.selectedRestaurants = [];
    this.selectedRestaurantNames = [];

    // Add selected restaurants from selected brands
    this.selectedBrands.forEach((brand) => {
      brand.restaurants.forEach((restaurant) => {
        if (restaurant.selected) {
          this.selectedRestaurants.push(restaurant);
          this.selectedRestaurantNames.push(restaurant.name);
        }
      });
    });
  }
}
