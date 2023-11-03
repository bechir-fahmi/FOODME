import { DeliveryAddressVM } from "./DeliveryAddressVM";
import { DeliveryType } from "./DeliveryType";
import { OrderElementVM } from "./OrderElementVM";
import { OrderItemVM } from "./OrderItemVM";
import { OrderStatus } from "./OrderStatus";
import { PaymentMode } from "./PaymentMode";
import { PaymentStatus } from "./PaymentStatus";

export class OrderVM{
    id: number = 0;
    orderNumber : string = '';
    userId:number=0;
    orderStatusDate:Date;
    deliveryFee:number=0;
    priceHT:number=0;
    tax:number=0;
    total:number=0;
    restaurantId:number=0;
    orderChannelType: string = '';
    customerNotes: string = '';
    driverNotes: string = '';
    deliveryAddress:DeliveryAddressVM;
    deliveryType:DeliveryType;
    paymentMode:PaymentMode;
    paymentStatus:PaymentStatus;
    orderStatus:OrderStatus;
    orderItems:OrderItemVM[];
    OrderElements:OrderElementVM[];
    processInstanceId: String = '';
    deliveryCompanyKey:  number =0 ; 
    externalOperatorKey:   number =0 ; 
    orderExternalOperatorId: string ='';
    posOrderId:number =0;
    creationTime:Date;
    customerPhoneNumber: string;
    customerCarNumber: string;
    customerCarType:string;
    customerCarColor: string;
    discount: number;
}



/**
 * {
  "id": 0,
  "orderNumber": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "processInstanceId": "string",
  "userId": "string",
  "deliveryAddress": {
    "id": 0,
    "orderId": 0,
    "latitude": 0,
    "longitude": 0,
    "addressId": 0,
    "fullAddress": "string"
  },
  "deliveryType": 1,
  "deliveryCompanyKey": 0,
  "paymentMode": 1,
  "paymentStatus": 0,
  "orderStatus": 0,
  "orderStatusDate": "2023-05-05T21:50:22.961Z",
  "deliveryFee": 0,
  "priceHT": 0,
  "tax": 0,
  "total": 0,
  "restaurantId": 0,
  "orderChannelType": "string",
  "customerNotes": "string",
  "driverNotes": "string",
  "orderItems": [
    {
      "id": 0,
      "quantity": 0,
      "idChooseableItem": 0,
      "version": 0,
      "elements": [
        {
          "id": 0,
          "quantity": 0,
          "idChooseableItem": 0,
          "version": 0
        }
      ],
      "modifierElements": [
        {
          "id": 0,
          "quantity": 0,
          "idModifierElement": 0,
          "version": 0,
          "modifierElements": [
            {
              "id": 0,
              "quantity": 0,
              "idChooseableItem": 0,
              "version": 0
            }
          ],
          "modifierAdjectives": [
            {
              "id": 0,
              "quantity": 0,
              "idAdjective": 0,
              "version": 0
            }
          ]
        }
      ],
      "modifierAdjectives": [
        {
          "id": 0,
          "quantity": 0,
          "idModifierAdjective": 0,
          "version": 0,
          "modifierElements": [
            {
              "id": 0,
              "quantity": 0,
              "idChooseableItem": 0,
              "version": 0
            }
          ],
          "modifierAdjectives": [
            {
              "id": 0,
              "quantity": 0,
              "idAdjective": 0,
              "version": 0
            }
          ]
        }
      ],
      "extraModifiers": [
        {
          "id": 0,
          "quantity": 0,
          "idExtraModifier": 0,
          "version": 0,
          "modifierElements": [
            {
              "id": 0,
              "quantity": 0,
              "idChooseableItem": 0,
              "version": 0
            }
          ],
          "modifierAdjectives": [
            {
              "id": 0,
              "quantity": 0,
              "idAdjective": 0,
              "version": 0
            }
          ]
        }
      ]
    }
  ],
  "orderElements": [
    {
      "id": 0,
      "quantity": 0,
      "idChooseableItem": 0,
      "version": 0
    }
  ],
  "externalOperatorKey": 0,
  "orderExternalOperatorId": "string",
  "posOrder": {
    "id": 0,
    "orderId": 0,
    "order": "string",
    "orderStatus": 0,
    "posOrderId": 0,
    "posCustomerInformation": {
      "id": 0,
      "userId": "string",
      "posCustomerId": 0,
      "posUserDeliveryAddressId": 0
    },
    "posStoreId": 0,
    "posDeliveryCode": 0,
    "posArea": 0,
    "licenseCode": "string",
    "conceptId": 0,
    "menuTemplateID": 0,
    "externalOperatorKey": 0,
    "externalOperatorId": "string",
    "forceSendToSDM": true,
    "businessKey": "string"
  },
  "creationTime": "2023-05-05T21:50:22.961Z",
  "customerPhoneNumber": "string",
  "customerCarNumber": "string",
  "customerCarType": "string",
  "customerCarColor": "string",
  "discount": 0
}
 */