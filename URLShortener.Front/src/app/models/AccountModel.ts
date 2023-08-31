export class AccountModel{
    constructor(id: string, username: string, jwtToken: string, role: string){
        this.id = id;
        this.username = username;
        this.jwtToken = jwtToken;
        this.role = role;
    }
    id: string = '';
    username: string = '';
    jwtToken: string = '';
    role: string = '';
}