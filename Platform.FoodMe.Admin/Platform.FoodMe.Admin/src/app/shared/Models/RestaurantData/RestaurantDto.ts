export interface RestaurantDto {
  id: number;
  nameLabelCode: string;
  contactName: string;
  contactPhone: string;
  contactMobile: string;
  contactEmail: string;
  addressLabelCode: string;
  delivery: boolean;
  pickup: boolean;
  carhop: boolean;
  deliveryTimeFrom: number;
  deliveryTimeTo: number;
  deliveryAmmount: number;
  deliveryMinAmmount: number;
  deliveryAreaKM: number;
  pickUpTime: number;
  isActive: boolean;
  isActiveHungerStation: boolean;
  takerProfileId: number;
  deliveryCompany: string;
  areaId: number;
}
