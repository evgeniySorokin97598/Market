
export class RegistrationModel{
    public name:string = "";
     
    public lastName:string = "";
    public city:string = "";
    public dateBirthday:Date = new Date();
    public isMan:boolean = false;
    public email = "";
    public password = "";
    public phone = "";
    public userName = ""
}
export class AuthorizationModel{
    public userName = "";
    public password = "";
}