import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../Models/Product';

@Component({
  selector: 'Add-Product',
  templateUrl: `AddProduct.component.html`,
})
export class AddProductComponent implements OnInit {

  private success: number;
  private _http: HttpClient;
  private _baseURL: string;
  private _addProduct: string;
  private name: string;
  private description: string;
  private price: number;
  private count: number;
  private nameErr: HTMLElement;
  private descErr: HTMLElement;
  private priceErr: HTMLElement;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseURL = baseUrl;
    this._http = http;
    this._addProduct = "product/AddProduct";
  }
  ngOnInit(): void {
    this.nameErr = document.getElementById("nameErr");
    this.descErr = document.getElementById("descErr");
    this.priceErr = document.getElementById("priceErr");
    this.name = '';
    this.description = '';
    this.price = 0;
    this.count = 1;
  }

  addProduct() {
    if (this.validateProduct()) {
      let product = new Product();
      product.Name = this.name;
      product.Description = this.description;
      product.Price = this.price;
      product.Count = this.count;
      this._http.post<any>(this._baseURL + this._addProduct,product).subscribe(result => {
        this.success = result[0];
      },
        error => console.error(error)
      );
    }
  }

  validateProduct() {
    this.nameErr.innerHTML = '';
    this.descErr.innerHTML = '';
    this.priceErr.innerHTML = '';
    if (this.name == '') {
      this.nameErr.innerHTML = 'Name cannot be empty.'
      return false;
    }
    else if (this.description == '') {
      this.descErr.innerHTML = 'Description cannot be empty.'
      return false;
    }
    else if (this.price == null) {
      this.priceErr.innerHTML = 'Price cannot be empty.'
      return false;
    }
    else if (this.price == 0) {
      this.priceErr.innerHTML = 'Price should be greater than 0.'
      return false;
    }
    return true;
  }

  clear() {
    this.name = '';
    this.description = '';
    this.price = 0;
  }
}
