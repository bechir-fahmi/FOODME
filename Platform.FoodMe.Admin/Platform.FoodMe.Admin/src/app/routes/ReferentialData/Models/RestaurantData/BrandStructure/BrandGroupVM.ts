import { SubscriptionFlowValidation } from "@shared/Models/SubscriptionFlowValidation";

export class BrandGroupVM{
  id: number = 0;
  nameLabelCode : string = '';
  email : string = '';
  phoneNumber : string = '';
  flowValidation : SubscriptionFlowValidation = SubscriptionFlowValidation.InValidation
}
