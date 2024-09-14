import { IProduct } from "./i-product";
import { IWarehouseProduct } from "./i-warehouse-product";

export interface IWarehouse {
    id: number,
    name: string,
    products: IProduct[],
    warehouseProducts: IWarehouseProduct[],
    totalProducts?: number,
    totalPrice?: number,
}
