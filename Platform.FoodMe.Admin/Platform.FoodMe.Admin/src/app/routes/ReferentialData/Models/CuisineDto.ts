export interface CuisineDto {
  id: number;
  nameLabelCode: string;
  imageLabelCode: string;
  nameLanguageResources?: NameLanguageResource[];
  imageFileResources?: imageFileResources[];
  status?: number;
}

export interface NameLanguageResource {
  id:          number;
  code:        string;
  value:       string;
  languageKey: number;
}
export interface imageFileResources {
  id:          number;
  code:        string;
  value:       string;
  languageKey: number;
}
