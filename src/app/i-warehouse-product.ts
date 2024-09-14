import { IProduct } from "./i-product";
import { IWarehouse } from "./i-warehouse";

export interface IWarehouseProduct {
    id: number,
    productId: number,
    product: IProduct,
    wareHouseId: number,
    warehouse: IWarehouse,
    productCount: number,
}
