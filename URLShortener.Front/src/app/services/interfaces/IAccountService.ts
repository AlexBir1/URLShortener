import { HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { AccountModel } from "src/app/models/AccountModel";
import { SignInModel } from "src/app/models/SignInModel";
import { SignUpModel } from "src/app/models/SignUpModel";
import { UpdateAccountModel } from "src/app/models/UpdateAccountModel";
import { IBaseResponse } from "../BaseResponse/IBaseResponse";

export interface IAccountService<T>{
    delete(id: string): Observable<IBaseResponse<T>>;
    update(id: string, entity: UpdateAccountModel): Observable<IBaseResponse<T>>;
    get(id: string): Observable<IBaseResponse<T>>;
    getCurrentAccount(): T;
    makeJWTHeader(): HttpHeaders;
    signUp(model: SignUpModel): Observable<IBaseResponse<T>>;
    signIn(model: SignInModel): Observable<IBaseResponse<T>>;
    refreshCurrentAccount(): void;
    saveCurrentAccount(entity: T): void;
    signOut(): void;
}