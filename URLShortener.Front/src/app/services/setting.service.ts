import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, ReplaySubject } from "rxjs";
import { environment } from "src/environments/environment";
import { SettingModel } from "../models/SettingModel";
import { BaseResponse } from "./BaseResponse/BaseResponse";
import { IBaseResponse } from "./BaseResponse/IBaseResponse";
import { ISettingService } from "./interfaces/ISettingService";

@Injectable()
export class SettingService implements ISettingService{
    private settingsSource = new ReplaySubject<SettingModel[] | null>(1);
    currentSettings$ = this.settingsSource.asObservable();
    url: string = environment.API_URL;

    constructor(private http: HttpClient) { }

    getAccountSetting(settingId: number, accountId: string): Observable<BaseResponse<SettingModel>> {
        return this.http.get<BaseResponse<SettingModel>>(this.url + 'api/setting/' + settingId + '/' + accountId);
    }
    updateAccountSetting(accountId: string, models: SettingModel[]): Observable<BaseResponse<SettingModel[]>> {
        return this.http.patch<BaseResponse<SettingModel[]>>(this.url + 'api/setting/' + accountId, models);
    }
    getAccountSettings(accountId: string): Observable<BaseResponse<SettingModel[]>> {
        return this.http.get<BaseResponse<SettingModel[]>>(this.url + 'api/setting/' + accountId);
    }

    addAccountSettings(accountId: string,setting: SettingModel[]){
        return this.http.post<BaseResponse<SettingModel[]>>(this.url + 'api/setting/' + accountId, setting);
    }

    insert(Entity: SettingModel): Observable<BaseResponse<SettingModel>> {
        throw new Error("Method not implemented.");
    }
    delete(id: number): Observable<IBaseResponse<SettingModel>> {
        throw new Error("Method not implemented.");
    }
    update(id: number, Entity: SettingModel): Observable<BaseResponse<SettingModel>> {
        throw new Error("Method not implemented.");
    }
    get(id: number): Observable<BaseResponse<SettingModel>> {
        throw new Error("Method not implemented.");
    }
    getAll(): Observable<BaseResponse<SettingModel[]>> {
        return this.http.get<BaseResponse<SettingModel[]>>(this.url + 'api/setting');
    }
    saveCurrentSettings(settings: SettingModel[]): void{
        localStorage.setItem(environment.userSettings, JSON.stringify(settings));
        this.settingsSource.next(settings);
        return;
    }
    deleteCurrentSettings(): void{
        localStorage.removeItem(environment.userSettings);
        this.settingsSource.next(null);
        return;
    }
    refreshSettings(accountId: string | null){
        if(accountId === null)
            return this.deleteCurrentSettings();
        this.getAccountSettings(accountId as string).subscribe(x=>{
            if(x.data){
                return this.saveCurrentSettings(x.data);
            }
            else 
                return this.deleteCurrentSettings();
        })
    }
}