import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ProductsService } from '../products.service';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [MatButtonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css'
})
export class AddProductComponent {

  createProductForm = this.fb.group({
    id: [0],
    productName: ['', Validators.required],
    price: [0, Validators.required],
  });

  constructor(
    private fb: FormBuilder, 
    private cdr: ChangeDetectorRef,
    private serviceProd: ProductsService) {}

    
  createProduct() {
    this.serviceProd.postProduct(this.createProductForm.value).subscribe({
        next : (value) => {
          console.log(value)
        },
        error : (err) => {
    console.log(err)
        }
      });
    }
}
