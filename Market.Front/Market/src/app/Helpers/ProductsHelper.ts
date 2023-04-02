import { Injectable } from "@angular/core";
import * as console from "console";
import { LocalStorageService } from "../Services/LocalStorageService";

@Injectable()
export class ProductsHelper{

    public numbers:number[] = [];
    
    keyLocalStorage:string = "products"; // ключ по которому нужно получать сериализованную коллекцию с товарами, которая будет использоваться в корзине

    constructor(private localStorageService:LocalStorageService){
        
    }

    public GetProducts(): number[]{
        /// если есть записи
      if (localStorage.length>0){
         
        /// получаем сериализованный json
          let json  = localStorage.getItem(this.keyLocalStorage)
        
          /// дессериализуем
          this.numbers= JSON.parse(json as string);
            
      }
      return this.numbers;
      
    }

    public RemoveProduct(id:number){
      this.GetProducts();
      let findId; // Itemitem(object here) to remove
      this.numbers.forEach((item,index) => {
        if (item == id) this.numbers.splice(index,1)
      })
      this.localStorageService.saveData(this.keyLocalStorage,JSON.stringify(this.numbers)) // сохраняем обратно с новым айдишником
    }
    public AddProduct(id:number){
      
        /// сперва загружаем то что есть
        this.GetProducts();
         
        /// добавление только в случае если ранее он не был добавен
        if (!this.numbers.includes(id) ){
            /// добавляем к тому что есть новый товар   
            this.numbers.push(id);
             
            this.localStorageService.saveData(this.keyLocalStorage,JSON.stringify(this.numbers)) // сохраняем обратно с новым айдишником
        }
        
    }
}