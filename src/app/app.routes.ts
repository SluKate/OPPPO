import { Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { AddProductComponent } from './add-product/add-product.component';

export const routes: Routes = [
    {path: '', redirectTo: 'products', pathMatch: 'full' },
    {path: 'products', component: ProductsComponent},
    {path: 'warehouse', component: WarehouseComponent},
    {path: 'add-product', component: AddProductComponent}
];
