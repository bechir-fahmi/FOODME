import { Status } from '@shared/Models/LocationData/CountryDto';
import { RoleClaimVM } from './RoleClaimVM';

export class RoleVM{
  id: string ;
  name: string ;
  status : Status = Status.isActive;
  creatorUserId : string ;
  creationTime: Date;
  lastModifierUserId : string;
  lastModificationTime: Date;
  claims : Array<RoleClaimVM> = [];
  DeleterUserId :string;
  DeletionTime :Date;
}

export class CreateRoleVM{
  name: string ;
  claims : Array<RoleClaimVM> = [];
}
