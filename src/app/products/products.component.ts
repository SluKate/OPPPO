import { ChangeDetectorRef, Component, Injectable } from '@angular/core';
import { ProductsService } from '../products.service';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormBuilder, FormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import { Validators, ReactiveFormsModule } from '@angular/forms';
import { IProduct } from '../i-product';
import {MatTableModule} from '@angular/material/table';
import {CurrencyPipe} from '@angular/common';

@Injectable({
  providedIn: 'root' // или в конкретном модуле
})
@Component({
  selector: 'app-products',
  standalone: true,
  imports: [MatInputModule, MatFormFieldModule, FormsModule, MatButtonModule, ReactiveFormsModule, MatTableModule, CurrencyPipe],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {

  productList: IProduct[] = []
  displayedColumns: string[] = ['id', 'name', 'price'];
  createProductForm = this.fb.group({
    id: [0],
    name: ['', Validators.required],
    price: [0, Validators.required],
  });

  constructor(
    private fb: FormBuilder, 
    private cdr: ChangeDetectorRef,
    private serviceProd: ProductsService
  ){
  }

  ngOnInit(): void {
    
    this.serviceProd.getProducts().subscribe({
      next: (value) => {
        this.productList = value 
        this.cdr.detectChanges();
      }
    })
  }

  getTotalCost() {
    return this.productList.map(t => t.price).reduce((acc, value) => acc + value, 0);
  }
}
