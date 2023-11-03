import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { MenuCategoryVM } from '../../Models/MenuData/MenuCategoryVM';



@Injectable({
  providedIn: 'root'
})
export class MenuCategoryService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllMenuCategories() {
    return this.http.get<MenuCategoryVM[]>(`${environment.API}/MenuCategory/GetAllMenuCategory`);
  }

  getMenuCategory(MenuCategoryId : number) {
    return this.http.get<MenuCategoryVM[]>(`${environment.API}/MenuCategory/GetMenuCategory/id/${MenuCategoryId}`);
  }


  addMenuCategory(MenuCategory: MenuCategoryVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify({
      "id": 0,
      "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "priority": 0,
      "rank": 0,
      "menuItemChildren": [
        {
          "id": 0,
          "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "menuCategoryId": 0,
          "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "nameLanguageResources": [
            {
              "id": 0,
              "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "value": "string",
              "languageKey": 0
            }
          ],
          "descriptionLanguageResources": [
            {
              "id": 0,
              "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "value": "string",
              "languageKey": 0
            }
          ],
          "priority": 0,
          "imageLink": "string",
          "calories": 0,
          "visibility": 0,
          "price": 0,
          "children": [
            {
              "id": 0,
              "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "nameLanguageResources": [
                {
                  "id": 0,
                  "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "value": "string",
                  "languageKey": 0
                }
              ],
              "descriptionLanguageResources": [
                {
                  "id": 0,
                  "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "value": "string",
                  "languageKey": 0
                }
              ],
              "priority": 0,
              "imageLink": "string",
              "calories": 0,
              "quantity": 0,
              "visibility": 0,
              "rank": 0,
              "price": 0,
              "modifierGroupId": 0
            }
          ],
          "extraModifierGroups": [
            {
              "maxQuantityOfModifier": 0,
              "minRequiredQuantityOfModifier": 0,
              "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "languageResources": [
                {
                  "id": 0,
                  "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "value": "string",
                  "languageKey": 0
                }
              ],
              "elements": [
                {
                  "id": 0,
                  "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "nameLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "descriptionLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "priority": 0,
                  "imageLink": "string",
                  "calories": 0,
                  "quantity": 0,
                  "visibility": 0,
                  "rank": 0,
                  "price": 0,
                  "modifierGroupId": 0
                }
              ],
              "adjectives": [
                {
                  "id": 0,
                  "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "priority": 0,
                  "imageLink": "string",
                  "calories": 0,
                  "quantity": 0,
                  "visibility": 0,
                  "rank": 0,
                  "version": 0
                }
              ],
              "modifierItems": [
                {
                  "id": 0,
                  "typeOfModifierItem": "string"
                }
              ],
              "menuItemId": 0,
              "id": 0
            }
          ],
          "modifierByAdjectives": [
            {
              "id": 0,
              "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "languageResources": [
                {
                  "id": 0,
                  "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "value": "string",
                  "languageKey": 0
                }
              ],
              "price": 0,
              "adjectives": [
                {
                  "id": 0,
                  "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "priority": 0,
                  "imageLink": "string",
                  "calories": 0,
                  "quantity": 0,
                  "visibility": 0,
                  "rank": 0,
                  "version": 0
                }
              ]
            }
          ],
          "modifierByElements": [
            {
              "id": 0,
              "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "languageResources": [
                {
                  "id": 0,
                  "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "value": "string",
                  "languageKey": 0
                }
              ],
              "price": 0,
              "menuElements": [
                {
                  "id": 0,
                  "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "nameLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "descriptionLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "priority": 0,
                  "imageLink": "string",
                  "calories": 0,
                  "quantity": 0,
                  "visibility": 0,
                  "rank": 0,
                  "price": 0,
                  "modifierGroupId": 0
                }
              ],
              "version": 0,
              "posModifierElement": {
                "posSystemId": 0,
                "posSystem": {
                  "id": 0,
                  "name": "string",
                  "posType": 0
                },
                "menuElementId": 0,
                "menuElement": {
                  "id": 0,
                  "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "nameLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "priority": 0,
                  "imageLink": "string",
                  "calories": 0,
                  "quantity": 0,
                  "visibility": 0,
                  "rank": 0,
                  "price": 0,
                  "pricingPolicy": {
                    "id": 0,
                    "typeOfPricingPolicy": 0,
                    "price": 0,
                    "currency": {
                      "id": 0,
                      "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
                    }
                  },
                  "posMenuElement": "string",
                  "modifierGroupId": 0,
                  "descriptionLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "version": 0,
                  "modifierElement": {
                    "id": 0,
                    "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "languageResources": [
                      {
                        "id": 0,
                        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                        "value": "string",
                        "languageKey": 0
                      }
                    ],
                    "price": 0,
                    "menuElements": [
                      "string"
                    ],
                    "version": 0,
                    "posModifierElement": "string"
                  }
                },
                "posMenuElementId": 0,
                "posLastModificationTime": "2023-04-27T21:53:30.825Z"
              }
            }
          ],
          "relativeMenuElementsModifierGroups": [
            {
              "id": 0,
              "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "languageResources": [
                {
                  "id": 0,
                  "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "value": "string",
                  "languageKey": 0
                }
              ],
              "price": 0,
              "menuElements": [
                {
                  "id": 0,
                  "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "descriptionLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                  "nameLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "descriptionLanguageResources": [
                    {
                      "id": 0,
                      "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                      "value": "string",
                      "languageKey": 0
                    }
                  ],
                  "priority": 0,
                  "imageLink": "string",
                  "calories": 0,
                  "quantity": 0,
                  "visibility": 0,
                  "rank": 0,
                  "price": 0,
                  "modifierGroupId": 0
                }
              ]
            }
          ]
        }
      ],
      "nameLanguageResources": [
        {
          "id": 0,
          "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "value": "string",
          "languageKey": 0
        }
      ],
      "descriptionLanguageResources": [
        {
          "id": 0,
          "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "value": "string",
          "languageKey": 0
        }
      ],
      "menuId": 0
    });
    return this.http.post(`${environment.API}/MenuCategory/AddMenuCategory`, body, {'headers':headers});
  }

  updateMenuCategory(MenuCategory: MenuCategoryVM) {
    return this.http.put(`${environment.API}/MenuCategory/UpdateMenuCategory`, MenuCategory)
  }

  deleteMenuCategory(MenuCategoryId: number) {
    return this.http.delete(`${environment.API}/MenuCategory/RemoveMenuCategory/${MenuCategoryId}`);
  }
}