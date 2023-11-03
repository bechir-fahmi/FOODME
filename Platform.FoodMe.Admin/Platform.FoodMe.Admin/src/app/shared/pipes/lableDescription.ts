import { HttpClient } from '@angular/common/http';
import { Pipe, PipeTransform } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
@Pipe({
  name: 'LableDescription'
})
export class LableDescription implements PipeTransform {
  constructor(private http: HttpClient) {}
  transform(ep: any): Observable<any> {
    return this.http.get(`${environment.API}/CarhopAddress/GetCarhopAddress/` + ep);
  }
}


