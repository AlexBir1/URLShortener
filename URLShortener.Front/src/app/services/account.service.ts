import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, of, ReplaySubject } from 'rxjs';

import { environment } from 'src/environments/environment';
import { AccountModel } from '../models/AccountModel';
import { SignUpModel } from '../models/SignUpModel';
import { SignInModel } from '../models/SignInModel';
import { UpdateAccountModel } from '../models/UpdateAccountModel';
import { IAccountService } from './interfaces/IAccountService';
import { BaseResponse } from './BaseResponse/BaseResponse';

@Injectable()
export class AccountService implements IAccountService<AccountModel>{
  private accountSource = new ReplaySubject<AccountModel | null>(1);
  currentAccount$ = this.accountSource.asObservable();
  private url: string = environment.API_URL;

  constructor(private http: HttpClient) {
  }
  saveCurrentAccount(entity: AccountModel): void {
    localStorage.setItem(environment.userKey, JSON.stringify(entity));
    this.accountSource.next(entity);
  }
  makeJWTHeader() {
    let jwt = this.getJWTToken();
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);
    return headers;
  }
  delete(id: string): Observable<BaseResponse<AccountModel>> {
    throw new Error('Method not implemented.');
  }
  update(id: string, entity: UpdateAccountModel): Observable<BaseResponse<AccountModel>> {
    var headers = this.makeJWTHeader();
    return this.http.put<BaseResponse<AccountModel>>(this.url + 'api/account/' + id, entity, { headers });
  }
  get(id: string): Observable<BaseResponse<AccountModel>> {
    throw new Error('Method not implemented.');
  }
  getCurrentAccount(): AccountModel {
    const key = localStorage.getItem(environment.userKey);
    const account: AccountModel = JSON.parse(key as string);
    return account;
  }
  signUp(model: SignUpModel): Observable<BaseResponse<AccountModel>> {
    return this.http.post<BaseResponse<AccountModel>>(this.url + 'api/account/SignUp', model);
  }
  signIn(model: SignInModel): Observable<BaseResponse<AccountModel>> {
    return this.http.post<BaseResponse<AccountModel>>(this.url + 'api/account/SignIn', model);
  }
  signOut(): void {
    localStorage.removeItem(environment.userKey);
    this.accountSource.next(null);
  }
  refreshCurrentAccount(): void {
    const jwt = this.getJWTToken();
    if (jwt) {
      this.getAndSaveAccount(jwt).subscribe({
        next: () => {
        },
        error: () => {
          this.signOut();
        }
      })
    }
    else {
      this.getAndSaveAccount(null).subscribe();
    }
  }

  private getAndSaveAccount(jwt: string | null) {
    if (jwt === null) {
      this.accountSource.next(null);
      return of(undefined);
    }
    else {
      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + jwt);
      return this.http.get<AccountModel>(this.url + 'api/account/refreshToken', { headers }).pipe(map((account: AccountModel) =>
      {
        if (account) {
          this.saveCurrentAccount(account);
        }
      }));
    }
  }

  private getJWTToken() {
    const key = localStorage.getItem(environment.userKey);
    if (key) {
      const account: AccountModel = JSON.parse(key);
      var tken = account.jwtToken;
      return tken;
    }
    else {
      return null;
    }
  }

}
