import { Observable } from "rxjs";
import { IBaseResponse } from "../BaseResponse/IBaseResponse";


export interface IService<T>{
    insert(Entity: T): Observable<IBaseResponse<T>>;
    delete(id: number): Observable<IBaseResponse<T>>;
    update(id: number, Entity: T): Observable<IBaseResponse<T>>;
    get(id: number): Observable<IBaseResponse<T>>;
    getAll(): Observable<IBaseResponse<T[]>>;
}