using Microsoft.EntityFrameworkCore;
using NewStorageLab.DAL;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;
using NewStorageLab.Domain.Exceptions;
using NewStorageLab.Domain.Services;

namespace NewStorageLab.WarehouseService;

public class WarehouseService : IWarehouseService
{
    private readonly AppDbContext _appDbContext;
    private readonly IProductService _productService;

    public WarehouseService(AppDbContext appDbContext, IProductService productService)
    {
        _appDbContext = appDbContext;
        _productService = productService;

    }

    public async Task<Warehouse> GetWarehouseAsync(int warehouseId)
    {
        var res = await _appDbContext.Warehouses
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == warehouseId);

        if (res is null)
        {
            throw new ValidationException($"склал с не найден");
        }

        return res;
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        var res = await _appDbContext.Warehouses.ToListAsync();

        return res;
    }

    public async Task<WarehouseStatsDTO> GetWarehouseStatsAsync(int warehouseId)
    {
        var warehouseProducts = await _appDbContext.WarehouseProducts
            .Where(x => x.WareHouseId == warehouseId)
            .Include(x => x.Product)
            .ToListAsync();
        var productsCount = warehouseProducts.Sum(x => x.ProductCount);

        double totalPrice = warehouseProducts.Sum(x => x.Product.Price * x.ProductCount);

        return new WarehouseStatsDTO
        {
            TotalPrice = totalPrice,
            TotalProducts = productsCount
        };
    }

    public async Task<Warehouse> CreateWarehouseAsync(WarehouseCreateDTO warehouseCreateDTO)
    {
        var warehouse = new Warehouse()
        {
            Name = warehouseCreateDTO.Name,
        };

        await _appDbContext.AddAsync(warehouse);

        await _appDbContext.SaveChangesAsync();

        return warehouse;
    }

    public async Task ReplenishWarehouseAsync(int warehouseId, WarehouseAdjustmentDTO deliveryDTO)
    {
        var wp = await _appDbContext.WarehouseProducts
            .FirstOrDefaultAsync(x => x.ProductId == deliveryDTO.ProductId && x.WareHouseId == warehouseId);

        if (wp is null)
        {
            wp = new WarehouseProduct()
            {
                ProductId = deliveryDTO.ProductId,
                WareHouseId = warehouseId,
                ProductCount = deliveryDTO.Count
            };

            await _appDbContext.AddAsync(wp);
        }
        else
        {
            wp.ProductCount += deliveryDTO.Count;

            _appDbContext.Update(wp);
        }

        await _appDbContext.SaveChangesAsync();
    }

    public async Task ShipFromWarehouseAsync(int warehouseId, WarehouseAdjustmentDTO shippingDTO)
    {
        var wp = await _appDbContext.WarehouseProducts
            .FirstOrDefaultAsync(x => x.ProductId == shippingDTO.ProductId && x.WareHouseId == warehouseId);

        if (wp is null)
        {
            throw new ValidationException("не найден склад или товар на складе");
        }

        if (shippingDTO.Count > wp.ProductCount)
        {
            throw new ValidationException("кол-во запрашиваемого товара превышает кол-во товара на складе");
        }

        wp.ProductCount -= shippingDTO.Count;

        if (wp.ProductCount != 0)
        {
            _appDbContext.Update(wp);
        }
        else
        {
            await RemoveProductFromWarehouseAsync(wp.Id);
        }

        await _appDbContext.SaveChangesAsync();
    }

    private async Task RemoveProductFromWarehouseAsync(int wpId)
    {
        var pw = await _appDbContext.WarehouseProducts.FirstOrDefaultAsync(x => x.Id == wpId);

        if (pw is null)
        {
            return;
        }

        _appDbContext.WarehouseProducts.Remove(pw);
        await _appDbContext.SaveChangesAsync();
    }
}
