import { Observable } from "rxjs/internal/Observable";
import { ShortURLInfoModel } from "src/app/models/ShortURLInfoModel";
import { ShortURLModel } from "src/app/models/ShortURLModel";
import { BaseResponse } from "../BaseResponse/BaseResponse";
import { IService } from "./IService";

export interface IShortURLService extends IService<ShortURLModel>{
    getDetailed(id: number): Observable<BaseResponse<ShortURLInfoModel>>;
} 