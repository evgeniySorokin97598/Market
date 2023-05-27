import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigurationService } from "../Services/ConfigService";

@Injectable()
export class HttpClientHelper{

  private static _headers: HttpHeaders = new HttpHeaders();
    public get headers(): HttpHeaders {
        return HttpClientHelper._headers;
    }
    public set headers(value: HttpHeaders) {
        HttpClientHelper._headers = value;
        
    }
    constructor(private http :HttpClient){

    }
    
    

    public async DeleteRequest(action:string,obj:any):Promise<any>{
        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
             
                
             let responce =   this.http.delete<any>(action,{headers: this.headers});
             
             responce.subscribe(
            (data ) => {
             resolve(data);
            },
            (error) => {
                console.log(error);
                alert(error);
              }
            );

        }));

        return await p;

    }

    public async PostRequest(action:string,obj:any):Promise<any>{
        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
            let rezult:any;

             let responce =   this.http.post<any>(action,obj,{headers: this.headers});
             
             responce.subscribe(
            (data ) => {
             resolve(data);
            },
            (error) => {
                console.log(error);
                
                
              }
            );

        }));

        return await p;

    }
    public async PutRequest(action:string,obj:any):Promise<any>{

        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
            let rezult:any;

             let responce =   this.http.put<any>(action,obj,{headers: this.headers});
             
             responce.subscribe(
                (data ) => {
                resolve(data);
                },
                (error) => {
                    console.log(error);
                    alert(error);
                }
            );

        }));

        return await p;

    }

    public async GetRequest(action:string):Promise<any>{

        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
            let rezult:any;

             let responce =   this.http.get<any>(action ,{headers: this.headers});
             
             responce.subscribe(
            (data ) => {
             resolve(data);
            },
            (error) => {
                console.log(error);
                 
              }
            );
        }));
            
        return await p;

    }

}