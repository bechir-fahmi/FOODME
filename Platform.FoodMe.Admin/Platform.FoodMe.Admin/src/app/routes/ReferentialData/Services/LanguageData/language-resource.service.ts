import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { languageResourceVM } from '../../Models/language-data/languageResourceVM';

import { HelperService } from '../helper.service';


@Injectable({
  providedIn: 'root'
})
export class LanguageResourceService {
  private http: HttpClient;
  private helper: HelperService<languageResourceVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();


  constructor(http: HttpClient, helper: HelperService<languageResourceVM>) { 
    this.http = http;
    this.helper = helper;
  }

  getLanguageResources() {
    return this.helper.get('/LanguageResource/GetAllLanguageResource');
  }

  getLanguageResourcesByCountryCode(countryCode: string) {
    return this.helper.get(`/LanguageResource/GetLanguageResourcesByCountryCode/${countryCode}`);
  }

  deleteLanguageResource(languageResourceId: number) {
    return this.helper.delete(`/LanguageResource/RemoveLanguage/${languageResourceId}`);
  }

  addLanguageResource(languageResource: languageResourceVM) {
    return this.helper.add('/LanguageResource/AddLanguageResource', languageResource);
  }

  updateLanguageResource(languageResource: languageResourceVM) {
  return this.helper.update('/LanguageResource/AddLanguageResource', languageResource);
  }

  sendCountryCode(message:string) {
    this.messageSource.next(message)
  }
  getCountryCode() {
    return this.messageSource.asObservable();
  }

}