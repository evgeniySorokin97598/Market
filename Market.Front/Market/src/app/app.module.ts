import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientHelper } from './Helpers/HttpClientHelper';
import { BaseService } from './Services/BaseService';
import { ProductsListComponent } from './Components/products-list/products-list.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
 
import { RouterModule, Routes } from '@angular/router';
import { ProductPageComponent } from './Components/product-page/product-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTabsModule} from '@angular/material/tabs';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import { ShoppingCartComponent } from './Components/shopping-cart/shopping-cart.component';
import { LocalStorageService } from './Services/LocalStorageService';
import { ProductsHelper } from './Helpers/ProductsHelper';
import { CategoryListComponent } from './Components/category-list/category-list.component';
import { FormsModule } from '@angular/forms';
import {MatTableModule} from '@angular/material/table';
const routes: Routes = [
  {path: 'Products/:SubCategoryName', component:ProductsListComponent},
  {path: 'Product/:id', component:ProductPageComponent},
  {path: 'ProductsBasket',component:ShoppingCartComponent},
  {path: 'Subcategories/:categoryName',component:CategoryListComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    ProductsListComponent,
    ProductPageComponent,
    ShoppingCartComponent,
    CategoryListComponent,
     
     
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatTabsModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    FormsModule,
    MatTableModule
  ],
  providers: [
    HttpClientHelper,
    BaseService,
    LocalStorageService,
    ProductsHelper
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  ngOnInit(): void {
`   `
      
  }

}
