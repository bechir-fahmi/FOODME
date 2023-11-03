export interface User {
  [prop: string]: any;

  Email?: string;
  UserName?: string;

  Picture?: string;
  Location?: string;
  PhoneNumber? : string;

  UserRoles?: any[];
}

export interface Token {
  [prop: string]: any;

  access_token: string;
  token_type?: string;
  exp : Date | null;
  refresh_token?: string;
}


export interface Response {
  [prop: string]: any;

  statusCodes: number;
  message: string;
  error: string;
  token: string
}
