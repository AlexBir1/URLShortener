export class ShortURLModel{
    constructor(id: number, url: string, origin: string, createdBy: string, createdByUserId: string = ''){
        this.id = id;
        this.url = url;
        this.origin = origin;
        this.createdBy = createdBy;
        this.createdByUserId = createdByUserId;
    }
    public id: number = 0;
    public url: string = '';
    public origin: string = '';
    public createdBy: string = '';
    public createdByUserId: string = '';
}