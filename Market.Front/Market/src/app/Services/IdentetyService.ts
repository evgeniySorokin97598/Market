import { HttpClient, HttpHeaders } from "@angular/common/http";
import { EventEmitter, Injectable } from "@angular/core";
import { AuthorizationModel, RegistrationModel } from "../Entities/RegistrationModel";
import { HttpClientHelper } from "../Helpers/HttpClientHelper";
import { ConfigurationService } from "./ConfigService";

@Injectable()
export class IdentetyService{
    _apiUrl = "";
    headers : HttpHeaders = new HttpHeaders();
   public IsAuthorize: boolean = false;

   public onAuth: EventEmitter<string> = new EventEmitter(); /// событие успешной авторизации

   constructor(private helper:HttpClientHelper,private configurationService: ConfigurationService){
    
   }
   public async Init():Promise <any>{
    this.configurationService.load();
   let result =   await Promise.resolve<any>(new Promise<any>((resolve, reject) =>{
     
     this.configurationService.getValue("identetyUrl").subscribe(data => {
         resolve(data);
         this._apiUrl = data
         console.log(data);
     });

  }));
   console.log("result: " + result);
   return await result;
 }

  public async Authorize(model:AuthorizationModel){
   await this.Init();
      let url = this._apiUrl +  "Authorization/login"
      let result = await this.helper.PostRequest(url,model)
      
      this.helper.headers = new HttpHeaders({
         'Content-Type': 'application/json',
         'Authorization': 'Bearer ' + result.token
      });
      this.onAuth.emit(model.userName);
      this.IsAuthorize = true;
  }

   public async Registration(model:RegistrationModel){
    await this.Init();
    let url:string = this._apiUrl + "Authorization/Registration"
     let result =   await this.helper.PostRequest(url,model);
      
     this.helper.headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + result.token
     });
     this.IsAuthorize = true;
     this.onAuth.emit(model.userName);
     
   }
}