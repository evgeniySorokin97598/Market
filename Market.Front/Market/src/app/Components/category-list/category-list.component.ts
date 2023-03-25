import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { Category } from 'src/app/Entities/Category';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BaseService } from 'src/app/Services/BaseService';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})


export class CategoryListComponent implements OnInit {
  hideComponent: boolean = true;
 
  private _category: Category = new Category(); 
  public get Category(): Category {
    return this._category;
  }

   
 

  constructor(private _router: Router,private router: Router,private _activeRoute: ActivatedRoute,private _service: BaseService) { 
    this.hideComponent= true;
     
  }

  async ngOnInit(): Promise<void> {
      console.log("init")
     let s = String(this._activeRoute.snapshot.paramMap.get("categoryName"));
     console.log("запрос данных по категории " + s);
     this.Category.subCategories = await this._service.GetSubcatories(s);

     this.router.events.subscribe( async (event) => {
      if (event instanceof NavigationStart) {
       
           
          
      }

      if (event instanceof NavigationEnd) {
          // Hide loading indicator
          let s = String(this._activeRoute.snapshot.paramMap.get("categoryName"));
          // Show loading indicator
        console.log("запрос данных по категории " + s);
        if (this.Category.categoryName != s){
          this.Category.subCategories = await this._service.GetSubcatories(s);
        }
        
      }

      if (event instanceof NavigationError) {
          // Hide loading indicator

          // Present error to user
          console.log(event.error);
      }
  });
  }

  
  
  ShowProducts(subcategory:string) {
    this.router.navigate(['/Products/' + subcategory]);
    this.hideComponent = false;
  }
}
