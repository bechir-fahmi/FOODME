import { Injectable } from '@angular/core';
import { BehaviorSubject, iif, merge, of } from 'rxjs';
import { catchError, map, share, switchMap, tap } from 'rxjs/operators';
import { TokenService } from './token.service';
import { LoginService } from './login.service';
import { filterObject, isEmptyObject } from './helpers';
import { User, Response, Token } from './interface';
//import { Token } from '@angular/compiler';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private user$ = new BehaviorSubject<User>({});
  private token : Token = {
    access_token: '',
    exp : null
  }
  private change$ = merge(
    this.tokenService.change(),
    this.tokenService.refresh().pipe(switchMap(() => this.refresh()))
  ).pipe(
    switchMap(() => this.assignUser()),
    share()
  );

  constructor(private loginService: LoginService, private tokenService: TokenService, private jwtHelper: JwtHelperService) {}

  init() {
    console.log("hi");

    //return new Promise<void>(resolve => this.change$.subscribe(() => resolve()));
  }

  change() {
    return this.change$;
  }

  check() {
    return this.tokenService.valid();
  }

  login(username: string, password: string, rememberMe = false) {
    return this.loginService.login(username, password, rememberMe).pipe(
      tap((response : Response)  => {
        console.log(this.jwtHelper.getTokenExpirationDate(response.token));
         this.token = {
          access_token : response.token,
          token_type : "Bearer",
          exp : this.jwtHelper.getTokenExpirationDate(response.token)
        }
        this.tokenService.set(this.token);
      }),
      map(() => this.check())
    );
  }

  refresh() {
    return this.loginService
      .refresh(filterObject({ refresh_token: this.tokenService.getRefreshToken() }))
      .pipe(
        catchError(() => of(undefined)),
        tap(token => this.tokenService.set(this.token)),
        map(() => this.check())
      );
  }

  logout() {
    return this.loginService.logout().pipe(
      tap(() => this.tokenService.clear()),
      map(() => !this.check())
    );
  }

  user() {
    return this.user$.pipe(share());
  }

  menu() {
    return iif(() => this.check(), this.loginService.menu(), of([]));
  }

  public assignUser() {
    if (!this.check()) {
      return of({}).pipe(tap(user => this.user$.next(user)));
    }

    if (!isEmptyObject(this.user$.getValue())) {
      return of(this.user$.getValue());
    }

    return this.loginService.me().pipe(tap(user => this.user$.next(user)));
  }

  createCompany() {
    // return this.loginServic.pipe(
    //   tap((response : Response)  => {
    //     console.log(this.jwtHelper.getTokenExpirationDate(response.token));
    //      this.token = {
    //       access_token : response.token,
    //       token_type : "Bearer",
    //       exp : this.jwtHelper.getTokenExpirationDate(response.token)
    //     }
    //     this.tokenService.set(this.token);
    //   }),
    //   map(() => this.check())
    // );
  }
}
