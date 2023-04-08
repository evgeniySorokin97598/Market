import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { RegistrationModel } from "../Entities/RegistrationModel";
import { HttpClientHelper } from "../Helpers/HttpClientHelper";
import { ConfigurationService } from "./ConfigService";

@Injectable()
export class IdentetyService{
    _apiUrl = "";
    headers : HttpHeaders = new HttpHeaders();;
    
   constructor(private helper:HttpClientHelper,private configurationService: ConfigurationService){
    
   }
   public async Init():Promise <any>{
    this.configurationService.load();
    /*
    let result = await ( this.configurationService.getValue("apiUrl").subscribe(data => {
       this._apiUrl = data
       console.log(data);
       console.log("return");
       return this._apiUrl;
   }));
   */
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

   public async Registration(model:RegistrationModel){
    await this.Init();
    let url:string = this._apiUrl + "Home/Registration"
     let result =   await this.helper.PostRequest(url,model);
     console.log(result);
     this.helper.headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + result.token
     });
     console.log("ТестАвториации");
      this.helper.GetRequest(this._apiUrl +"Home/TestAuth")

   }
}