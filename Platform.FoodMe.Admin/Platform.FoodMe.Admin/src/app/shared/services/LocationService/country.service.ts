import { environment } from '@env/environment';
import { CountryDTO } from '@shared/Models/LocationData/CountryDto';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

import * as moment from 'moment';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  //constructor(private http: HttpClient) { }

  private http: HttpClient;
  private baseUrl: string;
  // protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl ? baseUrl : "";
  }

  getcountries() {
    return this.http.get<CountryDTO[]>(`${environment.API}/Country/GetAllCountries`);
  }

  deletecountry(countryid: number) {
    return this.http.delete(`${environment.API}/Country?CountryId=${countryid}`);
  }

}