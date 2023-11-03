export enum Status {
  isDeleted = 0,
  isUpadate = 1,
  isActive = 2,
  isBlackListed = 3,
  isDefault = 4,
  isInactive = 5
}
export interface ReferentialDataBase {
  status: Status;
  creationTime: string | null;
  creatorUserId: number | null;
  deleterUserId: number | null;
  deletionTime: string | null;
  lastModifierUserId: number | null;
  lastModificationTime: string | null;
}
export interface CountryDTO extends ReferentialDataBase {
  id: number;
  nameLabelCode: string;
  code: string;
  countryKey: string;
}
