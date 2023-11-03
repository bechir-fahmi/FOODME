
import { CRUDPermissions } from "@shared/Models/Permissions/CRUDPermissions";
import { Modules } from "@shared/Models/Permissions/Modules";

export class RoleClaimVM{
  claimType : Modules = Modules.None;
  claimValue : Array<CRUDPermissions> = [];
}
