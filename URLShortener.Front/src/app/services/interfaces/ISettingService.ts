import { Observable } from "rxjs";
import { SettingModel } from "src/app/models/SettingModel";
import { IBaseResponse } from "../BaseResponse/IBaseResponse";
import { IService } from "./IService";

export interface ISettingService extends IService<SettingModel>{
    getAccountSettings(accountId: string): Observable<IBaseResponse<SettingModel[]>>;
    getAccountSetting(settingId: number, accountId: string): Observable<IBaseResponse<SettingModel>>;
    updateAccountSetting(accountId: string, models: SettingModel[]): Observable<IBaseResponse<SettingModel[]>>;
    addAccountSettings(accountId: string, setting: SettingModel[]): Observable<IBaseResponse<SettingModel[]>>;
}