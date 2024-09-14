import { ChangeDetectorRef, Component } from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { FormBuilder, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { WarehouseService } from '../warehouse.service';

@Component({
  selector: 'app-add-warehouse',
  standalone: true,
  imports: [MatButtonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-warehouse.component.html',
  styleUrl: './add-warehouse.component.css'
})
export class AddWarehouseComponent {

  createWareHouseForm = this.fb.group({
    id: [0],
    name: ['', Validators.required],
    price: [0, Validators.required],
  });

  constructor(
    private fb: FormBuilder, 
    private cdr: ChangeDetectorRef,
    private serviceWareHouse: WarehouseService) {}

    createWarehouse() {
      this.serviceWareHouse.addWarehouse(this.createWareHouseForm.value).subscribe({
        next: (value) => {
        }
      })
    }
}
