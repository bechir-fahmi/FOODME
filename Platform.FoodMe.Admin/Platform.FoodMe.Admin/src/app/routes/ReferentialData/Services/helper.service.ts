import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HelperService<S> {

  private http: HttpClient;
  REF_DATA_API_URL = environment.API;
  headers = { 'content-type': 'application/json' };

  constructor(http: HttpClient) {
    this.http = http;
   }

  error(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }


  get(url: string){
    return this.http.get<S[]>(`${this.REF_DATA_API_URL}${url}`, {headers:this.headers})
    .pipe(catchError(this.error));
  }

  delete(url: string){
    return this.http.delete(`${this.REF_DATA_API_URL}${url}`, {headers:this.headers})
    .pipe(catchError(this.error));
  }
  add(url: string, object: S){
    return this.http.post(`${this.REF_DATA_API_URL}${url}`, object, {headers:this.headers})
    .pipe(catchError(this.error));
  }
  update(url: string, object: S){
    return this.http.put(`${this.REF_DATA_API_URL}${url}`, object, {headers:this.headers})
    .pipe(catchError(this.error));
  }
}
