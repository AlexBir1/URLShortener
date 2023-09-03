import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { ShortURLInfoModel } from "../models/ShortURLInfoModel";
import { ShortURLModel } from "../models/ShortURLModel";
import { AccountService } from "./account.service";
import { BaseResponse } from "./BaseResponse/BaseResponse";
import { IBaseResponse } from "./BaseResponse/IBaseResponse";
import { IShortURLService } from "./interfaces/IShortURLService";

@Injectable()
export class ShortURLService implements IShortURLService{
    private url = environment.API_URL;
    constructor(private http: HttpClient, private accountService: AccountService) {
    }
    insert(entity: ShortURLModel): Observable<BaseResponse<ShortURLModel>> {
        var headers = this.accountService.makeJWTHeader();
        var newUrl = new ShortURLModel(0, entity.origin, entity.origin, entity.createdBy, entity.createdByUserId);
        return this.http.post<BaseResponse<ShortURLModel>>(this.url + 'api/ShortURL', newUrl, {headers});
    }
    delete(id: number): Observable<BaseResponse<ShortURLModel>> {
        var headers = this.accountService.makeJWTHeader();
    return this.http.delete<BaseResponse<ShortURLModel>>(this.url + 'api/ShortURL/' + id, {headers});
    }
    update(id: number, Entity: ShortURLModel): Observable<BaseResponse<ShortURLModel>> {
        throw new Error("Method not implemented.");
    }
    get(id: number): Observable<BaseResponse<ShortURLModel>> {
        throw new Error("Method not implemented.");
    }
    getDetailed(id: number): Observable<BaseResponse<ShortURLInfoModel>> {
        return this.http.get<BaseResponse<ShortURLInfoModel>>(this.url + 'api/ShortURL/' + id);
    }
    getAll(): Observable<IBaseResponse<ShortURLModel[]>> {
        return this.http.get<BaseResponse<ShortURLModel[]>>(this.url + 'api/ShortURL');
    }
}