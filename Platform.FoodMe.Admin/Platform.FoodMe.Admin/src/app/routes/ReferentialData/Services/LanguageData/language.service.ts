import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LanguageVM } from '../../Models/language-data/LanguageVM';
import { HelperService } from '../helper.service';


@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private http: HttpClient;
  private helper: HelperService<LanguageVM>;

  constructor(http: HttpClient, helper: HelperService<LanguageVM>) { 
    this.http = http;
    this.helper = helper;
  }

  getLanguages() {
    return this.helper.get('/Language/GetAllLanguages');
  }

  deleteLanguage(languageid: number) {
    return this.helper.delete(`/Language/RemoveLanguage/${languageid}`);
  }

  addLanguage(language: LanguageVM) {
    return this.helper.add('/Language/AddLanguage', language);
  }

  updateLanguage(language: LanguageVM) {
  return this.helper.update('/Language/UpdateLanguage', language);
  }
}