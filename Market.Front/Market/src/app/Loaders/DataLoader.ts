import { Category } from "../Entities/Category";
import { Product } from "../Entities/Product";
import { HttpClientHelper } from "../Helpers/HttpClientHelper";

export class DataLoader{
    private _apiUrl = "https://localhost:44315/";

    constructor (private _helper: HttpClientHelper){


    }
    public async GetHomePageData() : Promise<Category[]> {
        let url : string = this._apiUrl + "Home/GetHomePageData"
       return  await this._helper.GetRequest(url);

    }
    public async GetProductsBySubCategory(subcategory:string):Promise<Product[]>{
        let url = this._apiUrl + "Products/GetProductsByCategory/" + subcategory;
        return await this._helper.GetRequest(url);
    }
    public async GetProductById(id:number) : Promise<Product>{
        let url : string = this._apiUrl + "Products/GetProductById/" + id;
        return await this._helper.GetRequest(url);
    }

    public async GetGroductsById(id:number[]) :Promise<Product[]>{
        let url:string = this._apiUrl+ 'Products/GetProducts';
 
        
        console.log(id);
        return await this._helper.PostRequest(url,id)
    }
}