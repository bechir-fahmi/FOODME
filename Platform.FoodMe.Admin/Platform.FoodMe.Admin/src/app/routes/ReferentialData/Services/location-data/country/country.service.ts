import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { CountryVM } from 'app/routes/ReferentialData/Models/location-data/CountryVM';
import { catchError, Observable } from 'rxjs';
import { HelperService } from '../../helper.service';


@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private http: HttpClient;
  private helper: HelperService<CountryVM>;

  constructor(http: HttpClient, helper: HelperService<CountryVM>) { 
    this.http = http;
    this.helper = helper;
  }

  getcountries() {
    return this.helper.get('/Country/GetAllCountries');
  }

  deletecountry(countryid: number) {
    return this.helper.delete(`/Country/RemoveCountry/${countryid}`);
  }

  addCountry(country: CountryVM) {
    return this.helper.add('/Country/AddCountry', country);
  }

  updatecountry(country: CountryVM) {
  return this.helper.update('/Country/UpdateCountry', country);
  }
}
