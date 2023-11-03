import { base64, capitalize, currentTimestamp, timeLeft } from './helpers';
import { Token } from './interface';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Console } from 'console';

export abstract class BaseToken {
  constructor(protected attributes: Token, protected jwtHelper: JwtHelperService) {}

  get access_token(): string {
    return this.attributes.access_token;
  }

  get refresh_token(): string | void {
    return this.attributes.refresh_token;
  }

  get token_type(): string {
    return this.attributes.token_type ?? 'bearer';
  }

  get exp(): Date | null {
    return this.attributes.exp;
  }

  valid(): boolean {
    console.log(this.hasAccessToken() && !this.isExpired())
    return this.hasAccessToken() && !this.isExpired();
  }

  getBearerToken(): string {
    return this.access_token;
  }

  needRefresh(): boolean {
    return this.exp !== null && this.jwtHelper.isTokenExpired(this.access_token);
  }

  getRefreshTime(): number {
    return timeLeft(/*(this.exp ?? 0) -*/ 5);
  }

  private hasAccessToken(): boolean {
    return !!this.access_token;
  }

  private isExpired(): boolean {
    return this.exp !== null && this.jwtHelper.isTokenExpired(this.access_token);
  }
}

export class SimpleToken extends BaseToken {}

export class JwtToken extends SimpleToken {
  private _payload?: { exp?: Date | null };

  static is(accessToken: string): boolean {
    try {
      const [_header] = accessToken.split('.');
      const header = JSON.parse(base64.decode(_header));

      return header.typ.toUpperCase().includes('JWT');
    } catch (e) {
      return false;
    }
  }

  // get exp(): Date | null {
  //   return this.payload?.exp;
  // }

  private get payload(): { exp?: Date | null } {
    if (!this.access_token) {
      return {};
    }

    if (this._payload) {
      return this._payload;
    }

    const [, payload] = this.access_token.split('.');
    const data = JSON.parse(base64.decode(payload));
    if (!data.exp) {
      data.exp = this.attributes.exp;
    }

    return (this._payload = data);
  }
}
