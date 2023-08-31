export class ShortURLModel{
    constructor(id: number, url: string, origin: string, createdBy: string){
        this.id = id;
        this.url = url;
        this.origin = origin;
        this.createdBy = createdBy;
    }
    public id: number = 0;
    public url: string = '';
    public origin: string = '';
    public createdBy: string = '';
}