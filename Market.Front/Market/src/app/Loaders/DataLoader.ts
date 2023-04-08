import { Category, SubCategory } from "../Entities/Category";
import { Product } from "../Entities/Product";
import { HttpClientHelper } from "../Helpers/HttpClientHelper";
import { ConfigurationService } from "../Services/ConfigService";

export class DataLoader{
    private  _apiUrl:string = "";

    
    constructor (private _helper: HttpClientHelper,private configurationService: ConfigurationService){
         

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
        
        this.configurationService.getValue("apiUrl").subscribe(data => {
            resolve(data);
            this._apiUrl = data
            console.log(data);
        });

     }));
      console.log("result: " + result);
      return await result;
    }

    public async GetHomePageData() : Promise<Category[]> {
       let test =  await this.Init();
       console.log("Test " +test );
       let url : string = test + "Home/GetHomePageData"
       return  await this._helper.GetRequest(url);

    }
    public async GetProductsBySubCategory(subcategory:string):Promise<Product[]>{
        await this.Init();
        let url = this._apiUrl + "Products/GetProductsByCategory/" + subcategory;
        return await this._helper.GetRequest(url);
    }
    public async GetProductById(id:number) : Promise<Product>{
        await this.Init();
        let url : string = this._apiUrl + "Products/GetProductById/" + id;
        return await this._helper.GetRequest(url);
    }

    public async GetGroductsById(id:number[]) :Promise<Product[]>{
        await this.Init();
        let url:string = this._apiUrl+ 'Products/GetProducts';
        console.log(id);
        return await this._helper.PostRequest(url,id)
    }
    public async GetSubcatories(category:string):Promise<SubCategory[]>{
        await this.Init();
        let url:string = this._apiUrl+ 'Categories/GetSubCategories/'+category;
        return await this._helper.GetRequest(url)
    }
}