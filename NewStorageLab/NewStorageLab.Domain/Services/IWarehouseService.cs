using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;

namespace NewStorageLab.Domain.Services;

public interface IWarehouseService
{
    Task<Warehouse> GetWarehouseAsync(int warehouseId);
    Task<IEnumerable<Warehouse>> GetWarehousesAsync();
    Task<WarehouseStatsDTO> GetWarehouseStatsAsync(int warehouseId);
    Task<Warehouse> CreateWarehouseAsync(WarehouseCreateDTO warehouseCreateDTO);
    Task ReplenishWarehouseAsync(int warehouseId, WarehouseAdjustmentDTO deliveryDTO);
    Task ShipFromWarehouseAsync(int warehouseId, WarehouseAdjustmentDTO shippingDTO);
}
