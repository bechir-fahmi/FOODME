import { LanguageKey } from "./languageKey";

export class LanguageResourceDto {
    id: number = 0;
    code: string = '';
    value: string = '';
    languageKey:LanguageKey;
}