export class ShortURLInfoModel{
    constructor(id: number, url:string,origin:string,createdBy:string,creationDate:Date){
        this.id = id;
        this.url = url;
        this.origin = origin;
        this.createdBy = createdBy;
        this.creationDate = creationDate;
    }
    public id: number = 0;
    public url: string = '';
    public origin: string = '';
    public createdBy: string = '';
    public creationDate: Date;
}