import { Component, OnInit } from '@angular/core';
import { ProductsHelper } from 'src/app/Helpers/ProductsHelper';
import { BaseService } from 'src/app/Services/BaseService';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  constructor(private helper:ProductsHelper,private _service: BaseService) { }

  async ngOnInit(): Promise<void> {
    this._service.GetProductsById(this.helper.GetProducts())
  }

}
