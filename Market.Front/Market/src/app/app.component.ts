import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { Console } from 'console';
import { Category } from './Entities/Category';
import { HttpClientHelper } from './Helpers/HttpClientHelper';
import { BaseService } from './Services/BaseService';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Market';
  Categories: Category[] = [];
  public category: Category = new Category();
  hiddenCategories = true;
  displayedColumns: string[] = ['position', ];
  constructor(private _service: BaseService,private router: Router,private offcanvasService: NgbOffcanvas){


  }
  async ngOnInit(): Promise<void>{
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
