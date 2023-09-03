import { IBaseResponse } from "./IBaseResponse";

export class BaseResponse<T> implements IBaseResponse<T>{
    constructor(data: T, errors: string[]){
        this.data = data;
        this.errors = errors;
    }
    data: T;
    errors: string[] = [];

}