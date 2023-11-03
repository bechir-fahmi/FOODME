export class OrderFilterVM{
    id:number=0;
    name:string;
    description:string;
    orderStatusFilter : number=0;
    deliveryTypeFilter : number= 0;
    orderBrandFilter : number=0;
    orderRestaurantFilter :  number=0;
    orderRegionFilter : number=0;
    orderCityFilter: number =0;
    orderCompanyFilter :  number = 0 ; 
    restaurantNameFilter :  String ;
    orderFromDateFilter :Date;
    orderToDateFilter :Date;
    paymentModeFilter : number =0;
    paymentStatusFilter :  number = 0;
    agentIdFilter :number = 0; 
    orderStatusIds :  String; 
    deliveryTypeIds :  string;
    withoutSDMIdFilter :  boolean; 
    payLaterFilter : boolean; 
    withTotalServicesTime40FilterActive :  boolean; 
    withTotalServicesTime60FilterActive :  boolean;
    findingDriverthen2min : boolean;
    preparingthen20min : boolean;
    withoutSDMIdthen30second :boolean; 
    orderIdFilter : string;
    PhoneNumberFilter:string;

}