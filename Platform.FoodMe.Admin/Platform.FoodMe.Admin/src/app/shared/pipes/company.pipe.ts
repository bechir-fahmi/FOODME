import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'company'
})
export class CompanyPipe implements PipeTransform {

  transform(companys: any, searchText: any, item: string): any {

    if (searchText == null) return companys;
    switch (item) {
      case "restaurant": {
        return companys.filter(function (company: { restaurant: string; }) {
          return company.restaurant.toLowerCase().indexOf(searchText.toLowerCase()) > -1;
        })
      }
      case "company": {
        return companys.filter(function (company: { deliv_Comp: string; }) {
          return company.deliv_Comp.toLowerCase().indexOf(searchText.toLowerCase()) > -1;
        })
      }
      case "number": {
        return companys.filter(function (company: { number: string; }) {
          return company.number.toLowerCase().indexOf(searchText.toLowerCase()) > -1;
        })
      }

    }
  }
}
