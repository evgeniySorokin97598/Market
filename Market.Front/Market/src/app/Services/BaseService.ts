import { Injectable } from "@angular/core";
import { Category } from "../Entities/Category";
import { Product } from "../Entities/Product";
import { HttpClientHelper } from "../Helpers/HttpClientHelper";
import { DataLoader } from "../Loaders/DataLoader";

@Injectable()
export class BaseService{

    private _dataLoader:DataLoader; /// тут должен быть интерфейс

    constructor(helper:HttpClientHelper){
        this._dataLoader = new DataLoader(helper);


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

}