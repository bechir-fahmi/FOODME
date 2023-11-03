
export interface WorkingTime  {
  id: number
  restaurantId: number
  usualDailyWorkingTimes: UsualDailyWorkingTime[]
  exceptionalDailyWorkingTimes: ExceptionalDailyWorkingTime[]
}

export interface MorningStartTime {
  ticks: number
}

export interface MorningCloseTime {
  ticks: number
}

export interface AfterNoonStartTime {
  ticks: number
}

export interface AfterNoonCloseTime {
  ticks: number
}


export interface UsualDailyWorkingTime {
  id: number;
  day: number;
  morningStartTime: { ticks: number };
  morningCloseTime: { ticks: number };
  afterNoonStartTime: { ticks: number };
  afterNoonCloseTime: { ticks: number };
  isClosed: boolean;
  weeklyWorkingTimeWithExceptionsId: number;
}

export interface ExceptionalDailyWorkingTime {
  id: number;
  dateTime: string;
  nameLabelCode: string;
  descriptionLabelCode: string;

}

interface LanguageResource {
  id: number;
  code: string;
  value: string;
  languageKey: number;
}

