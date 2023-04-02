import { Component, TemplateRef } from '@angular/core';
import { MAT_DATE_RANGE_SELECTION_STRATEGY } from '@angular/material/datepicker';
import { Router } from '@angular/router';
import { NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { Console } from 'console';
import { FiveDayRangeSelectionStrategy } from './Components/shopping-cart/shopping-cart.component';
import { Category } from './Entities/Category';
import { HttpClientHelper } from './Helpers/HttpClientHelper';
import { ProductsHelper } from './Helpers/ProductsHelper';
import { BaseService } from './Services/BaseService';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    {
        provide: MAT_DATE_RANGE_SELECTION_STRATEGY,
        useClass: FiveDayRangeSelectionStrategy,
    },
],
})
export class AppComponent {
  title = 'Market';
  Categories: Category[] = [];
  public category: Category = new Category();
  hiddenCategories = true;
  displayedColumns: string[] = ['position', ];
  public hiddenCountCart = true;
  

  constructor(private _service: BaseService,private router: Router,private offcanvasService: NgbOffcanvas,public productsHelper:ProductsHelper,){
    productsHelper.GetProducts(); /// что бы на фронте сразу кол-во товаров отображалось, а не после какого то заказа
     
  }
  async ngOnInit(): Promise<void>{
    await this._service.Init();
    let categories =  await this._service.GetHomePageData();
    console.log(categories);
    this.Categories = categories;
    
  }
  ShowProducts(subcategory:string) {
    this.router.navigate(['/Products/' + subcategory]);
  }

  ShowProductsBasket(){
    this.router.navigate(['/ProductsBasket/']);
  }

  openScroll(content: TemplateRef<any>) {
		this.offcanvasService.open(content, { scroll: true });
	}
  ShowCategory(name:string){
 
    console.log(name);
    this.router.navigate(['/Subcategories/'+ name]);
  }
}
