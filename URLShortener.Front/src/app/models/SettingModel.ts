export class SettingModel{
    constructor(id: number, account_id: string, key: string, title: string, description: string, isActive: boolean){
        this.id = id;
        this.account_id = account_id;
        this.key = key;
        this.title = title;
        this.description = description;
        this.isActive = isActive;
    }
    public id: number;
    public account_id: string;
    public key: string;
    public title: string;
    public description: string;
    public isActive: boolean;
}