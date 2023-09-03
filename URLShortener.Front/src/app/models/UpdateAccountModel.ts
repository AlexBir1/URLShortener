export class UpdateAccountModel{
    constructor(){

    }
    public id: number = 0;
    public username: string = '';
    public role: string = '';
    public oldPassword: string = '';
    public newPassword: string = '';
    public confirmNewPassword: string = '';
}