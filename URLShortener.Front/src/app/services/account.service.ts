import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, of, ReplaySubject } from 'rxjs';

import { environment } from 'src/environments/environment';
import { ResponseModel } from '../models/ResponseModel';
import { AccountModel } from '../models/AccountModel';
import { SignUpModel } from '../models/SignUpModel';
import { SignInModel } from '../models/SignInModel';
import { UpdateAccountModel } from '../models/UpdateAccountModel';

@Injectable()
export class AccountService {
  private accountSource = new ReplaySubject<AccountModel | null>(1);
  currentAccount$ = this.accountSource.asObservable();


  private url: string = environment.API_URL;
  constructor(private http: HttpClient) {
  }

  refreshAccount() {
    const jwt = this.getJWTToken();
    if (jwt) {
      this.getAndSaveAccount(jwt).subscribe({
        next: () => {
        },
        error: () => {
          this.SignOut();
        }
      })
    }
    else {
      this.getAndSaveAccount(null).subscribe();
    }
  }

  getAndSaveAccount(jwt: string | null) {
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
          this.saveAccount(account);
        }
      }));
    }
  }

  getAccountId() {
    const key = localStorage.getItem(environment.userKey);
    if (key) {
      const account: AccountModel = JSON.parse(key);
      var accountId = account.id;
      return accountId;
    }
    else {
      return null;
    } 
  }

  makeJWTHeader() {
    let jwt = this.getJWTToken();
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);
    return headers;
  }

  SignOut() {
    localStorage.removeItem(environment.userKey);
    this.accountSource.next(null);
  }

  getJWTToken() {
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

  SignUp(registerModel: SignUpModel) {
    return this.http.post<ResponseModel<AccountModel>>(this.url + 'api/account/SignUp', registerModel);
  }

  SignIn(loginModel: SignInModel) {
    return this.http.post<ResponseModel<AccountModel>>(this.url + 'api/account/SignIn', loginModel);
  }

  saveAccount(accountModel: AccountModel) {
    localStorage.setItem(environment.userKey, JSON.stringify(accountModel));
    this.accountSource.next(accountModel);
  }

  updateAccount(model: UpdateAccountModel){
    var headers = this.makeJWTHeader();
    return this.http.put<ResponseModel<AccountModel>>(this.url + 'api/account/' + model.id, model, { headers });
  }
}
