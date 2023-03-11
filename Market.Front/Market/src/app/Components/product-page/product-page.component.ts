import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { Product } from 'src/app/Entities/Product';
import { BaseService } from 'src/app/Services/BaseService';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {
  public Product:Product = new Product();
  //images  = [944, 1011, 984].map((n) => `https://picsum.photos/id/${n}/900/500`);
  images:string[] = [];
  currentRate:number = 2;
  constructor(private _router: ActivatedRoute,private _service: BaseService,config: NgbRatingConfig) { 

    config.max = 5;
  }

  

  async ngOnInit(): Promise<void> {
    let s = Number(this._router.snapshot.paramMap.get("id"));
    
    this.Product = await this._service.GetProductById(s);
    this.images.push(this.Product.image)
    console.log(this.Product);
  }

}
