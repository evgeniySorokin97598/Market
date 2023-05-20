import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { CommentEntity, Product } from 'src/app/Entities/Product';
import { ProductsHelper } from 'src/app/Helpers/ProductsHelper';
import { BaseService } from 'src/app/Services/BaseService';
import { IdentetyService } from 'src/app/Services/IdentetyService';
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
  comment:CommentEntity = new CommentEntity();

  constructor(private _router: ActivatedRoute,private _service: BaseService,private _identetyService :IdentetyService, config: NgbRatingConfig, private helper:ProductsHelper,private modalService: NgbModal) { 

    config.max = 5;
    config.readonly = true;
  }

  

  async ngOnInit(): Promise<void> {
    let s = Number(this._router.snapshot.paramMap.get("id"));
    this.Product = await this._service.GetProductById(s);
    this.Product.id = s;
    console.log(this.Product.id);
    this.images.push(this.Product.image);
    console.log(this.Product);
    this.comment.productId = this.Product.id;
    console.log(this.comment.stars);
  }

  async SendComment(){
    this._service.SendComment(this.comment);
    this.comment = new CommentEntity();
  }

  /// добавление товара в корзину
  public AddToCart(){
    
    this.helper.AddProduct(this.Product.id);
  }
  public LikeComment( comment:CommentEntity){
    
  }
  open(content:any) {
    if (this._identetyService.IsAuthorize){
      const modalRef = this.modalService.open(content).result.then(
        (result) => {
           
        },
        (reason) => {
           
        },
      );
    }
		else alert("Сперва авторизуйтесь");
	 
	}
}
