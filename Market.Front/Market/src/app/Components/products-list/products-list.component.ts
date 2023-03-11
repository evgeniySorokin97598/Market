import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/Entities/Product';
import { BaseService } from 'src/app/Services/BaseService';
import { NgbRatingConfig, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  Products:Product[] = [];
  constructor(private router: ActivatedRoute,private _service: BaseService,config: NgbRatingConfig,private _router: Router) { 
    config.max = 5;
    config.readonly = true;
  }
  currentRate:number = 2;
  async ngOnInit(): Promise<void> {
    let s = String(this.router.snapshot.paramMap.get("SubCategoryName"));
    this.Products = await this._service.GetProducts(s);
  }

  public OpenProduct(id:number){
    console.log(id);
    this._router.navigate(['/Product/' + id]);
  }
  
}
