import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { Product } from 'src/app/Entities/Product';
import { ProductsHelper } from 'src/app/Helpers/ProductsHelper';
import { BaseService } from 'src/app/Services/BaseService';
import { LocalStorageService } from 'src/app/Services/LocalStorageService';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})

export class ProductPageComponent implements OnInit {
  public Product:Product = new Product();
   
  images:string[] = [];
  currentRate:number = 2;
   

  constructor(private _router: ActivatedRoute,private _service: BaseService,config: NgbRatingConfig, private helper:ProductsHelper) { 

    config.max = 5;
  }

  

  async ngOnInit(): Promise<void> {
    let s = Number(this._router.snapshot.paramMap.get("id"));
    
    this.Product = await this._service.GetProductById(s);
    this.Product.id = s;
    console.log(this.Product.id)
    this.images.push(this.Product.image)
    console.log(this.Product);
  }

  /// добавление товара в корзину
  public AddToCart(){

    this.helper.AddProduct(this.Product.id);
  }
}
