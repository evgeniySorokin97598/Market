import { DataSource } from '@angular/cdk/collections';
import { Component, Injectable, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { DateRange, MatDateRangeSelectionStrategy } from '@angular/material/datepicker';
import { Router } from '@angular/router';
import { Observable, ReplaySubject } from 'rxjs';
import { Product } from 'src/app/Entities/Product';
import { ProductsHelper } from 'src/app/Helpers/ProductsHelper';
import { BaseService } from 'src/app/Services/BaseService';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  Products:Product[] = [];
   public range:FormGroup;
   displayedColumns: string[] = ['name','price','quantity','remove'];
   dataSource = new ExampleDataSource(this.Products);

  constructor(private helper:ProductsHelper,private _service: BaseService,private _router: Router) { 
    
    this.range = new FormGroup({
      
      start: new FormControl(new Date(), [
        Validators.required
      ]),
      end: new FormControl(new Date(), [
        Validators.required
      ]),
    });

  }

  async ngOnInit(): Promise<void> {
    console.log("Кол-во товаров " + this.helper.GetProducts().length);
   this.Products = await  this._service.GetProductsById(this.helper.GetProducts());
   this.Products.forEach(p => console.log(p))
   console.log(this.Products.length)
   this.dataSource.setData(this.Products)
  }

  public OpenProduct(id:number){
    console.log(id);
    this._router.navigate(['/Product/' + id]);
  }
  
  public async RemoveItem(product:Product){
    this.helper.RemoveProduct(product.id);
    let index = this.Products.indexOf(product);
    this.Products.splice(index,1);
    this.dataSource.setData(this.Products);

  }
}
@Injectable()
export class FiveDayRangeSelectionStrategy<D> 
    implements MatDateRangeSelectionStrategy<D> {
    constructor(private _dateAdapter: DateAdapter<D>) { }
  
    selectionFinished(date: D | null): DateRange<D> {
        return this._createFiveDayRange(date);
    }
  
    createPreview(activeDate: D | null): DateRange<D> {
        return this._createFiveDayRange(activeDate);
    }
  
    private _createFiveDayRange(date: D | null): DateRange<D> {
        if (date) {
            const start = this._dateAdapter.addCalendarDays(date, -2);
            const end = this._dateAdapter.addCalendarDays(date, 2);
            return new DateRange < D > (start, end);
        }
  
        return new DateRange < D > (null, null);
    }
}
class ExampleDataSource extends DataSource<Product> {
  private _dataStream = new ReplaySubject<Product[]>();

  constructor(initialData: Product[]) {
    super();
    this.setData(initialData);
  }

  connect(): Observable<Product[]> {
    return this._dataStream;
  }

  disconnect() {}

  setData(data: Product[]) {
    this._dataStream.next(data);
  }
}
