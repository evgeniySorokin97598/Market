import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class HttpClientHelper{

    constructor(private http :HttpClient){
    }

    public async DeleteRequest(action:string,obj:any):Promise<any>{
        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
             
                
             let responce =   this.http.delete<any>(action);
             
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

    public async PostRequest(action:string,obj:any):Promise<any>{
        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
            let rezult:any;

             let responce =   this.http.post<any>(action,obj);
             
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

             let responce =   this.http.put<any>(action,obj);
             
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

    public async GetRequest(action:string):Promise<any>{

        let p  = await Promise.resolve<any>(new Promise<any>((resolve, reject) => {
            let rezult:any;

             let responce =   this.http.get<any>(action );
             
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