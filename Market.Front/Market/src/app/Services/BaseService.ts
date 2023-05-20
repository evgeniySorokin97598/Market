import { Injectable } from "@angular/core";
import { Category, SubCategory } from "../Entities/Category";
import { CommentEntity, Product } from "../Entities/Product";
import { HttpClientHelper } from "../Helpers/HttpClientHelper";
import { DataLoader } from "../Loaders/DataLoader";
import { ConfigurationService } from "./ConfigService";

@Injectable()
export class BaseService{

    private _dataLoader:DataLoader; /// тут должен быть интерфейс

    constructor(helper:HttpClientHelper,private configurationService: ConfigurationService){
        this._dataLoader = new DataLoader(helper,configurationService);
        

    }
    public Init(){
        this._dataLoader.Init();
        

    }
    public async GetHomePageData(): Promise<Category[]>{
       return await this._dataLoader.GetHomePageData();

    }
    public async GetProducts(subcategory:string):Promise<Product[]>{
        return await this._dataLoader.GetProductsBySubCategory(subcategory);

    }
    public async GetProductById(id:number):Promise<Product>{
        return await this._dataLoader.GetProductById(id);

    }
    public async GetProductsById(id:number[]):Promise<Product[]>{
        return await this._dataLoader.GetGroductsById(id);

    }
    public async GetSubcatories(category:string):Promise<SubCategory[]>{
        return await this._dataLoader.GetSubcatories(category);
    }
    public async SendComment(comment:CommentEntity ){
        return await this._dataLoader.SendComment(comment);
    }

}