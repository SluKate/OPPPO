import { ChangeDetectorRef, Component } from '@angular/core';
import { WarehouseService } from '../warehouse.service';
import { IWarehouse } from '../i-warehouse';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import { IProduct } from '../i-product';
import { ProductsService } from '../products.service';

@Component({
  selector: 'app-warehouse',
  standalone: true,
  imports: [MatTableModule, MatInputModule, MatFormFieldModule, FormsModule, MatIconModule, MatSelectModule],
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css'] // Исправлено на styleUrls
})
export class WarehouseComponent {

  productList: IProduct[] = []
  wareHouseList: IWarehouse[] = [];
  displayedColumns: string[] = ['id', 'name', 'totalProducts', 'totalPrice', 'addProduct',];

  constructor(
    private cdr: ChangeDetectorRef,
    private serviceWareHouse: WarehouseService,
    private serviceProd: ProductsService
  ) { }

  ngOnInit(): void {
    this.getProducts()
    this.serviceWareHouse.getWarehouse().subscribe({
      next: (warehouses: IWarehouse[]) => {
        this.wareHouseList = warehouses;

        // Получение общей информации для каждого склада
        warehouses.forEach(warehouse => {
          this.serviceWareHouse.getWarehouseTotal(warehouse.id).subscribe({
            next: (totalInfo) => {
              // Обновление информации о складе
              warehouse.totalProducts = totalInfo.totalProducts;
              warehouse.totalPrice = totalInfo.totalPrice;
            },
            error: (err) => {
              console.error(`Ошибка при получении данных для склада ${warehouse.id}:`, err);
            }
          });
        });

        this.cdr.detectChanges(); // Если необходимо, но может быть не нужно
      },
      error: (err) => {
        console.error('Ошибка при получении складов:', err);
      }
    });
  }

  
  getProducts(){
    this.serviceProd.getProducts().subscribe({
      next: (value) => {
        this.productList = value 
        this.cdr.detectChanges();
      }
    })
  }
}
