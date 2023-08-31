export class SignUpModel{
    constructor(username: string,  password: string, passwordConfirm: string){
        this.username = username;
        this.password = password;
        this.passwordConfirm = passwordConfirm;
    }
    username: string = '';
    password: string = '';
    passwordConfirm: string = '';
}