import { Injectable } from "@angular/core";
import { LocalStorageService } from "../Services/LocalStorageService";

@Injectable()
export class ProductsHelper{

    private numbers:number[] = [];
    keyLocalStorage:string = "products"; // ключ по которому нужно получать сериализованную коллекцию с товарами, которая будет использоваться в корзине
    constructor(private localStorageService:LocalStorageService){
        
    }

    public GetProducts(): number[]{
        /// если есть записи
      if (localStorage.length>0){
        console.log('получаем сериализованный json');
        /// получаем сериализованный json
          let json  = localStorage.getItem(this.keyLocalStorage)
          console.log("json " + json);
          /// дессериализуем
          this.numbers= JSON.parse(json as string);
          console.log(this.numbers.length)
      }
      return this.numbers;
      
    }

    public AddProduct(id:number){
      
        /// сперва загружаем то что есть
        this.GetProducts();

        /// добавление только в случае если ранее он не был добавен
        if (!this.numbers.includes(id) ){
            /// добавляем к тому что есть новый товар   
            this.numbers.push(id);
            console.log("Добавление нового продукта");
            this.localStorageService.saveData(this.keyLocalStorage,JSON.stringify(this.numbers)) // сохраняем обратно с новым айдишником
        }
        
    }
}