export class ResponseModel<T>{
    data!: T;
    errors: string [] = [];
}