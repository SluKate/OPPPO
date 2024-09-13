using Microsoft.EntityFrameworkCore;
using NewStorageLab.DAL;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;
using NewStorageLab.WarehouseService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class WarehouseServiceTests
{
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetWarehousesAsync_Returns_AllWarehouses()
    {
        var context = CreateInMemoryDbContext();
        var service = new WarehouseService(context, null);
        context.Warehouses.AddRange(
            new Warehouse { Id = 1, Name = "Warehouse 1" },
            new Warehouse { Id = 2, Name = "Warehouse 2" }
        );
        context.SaveChanges();

        var result = await service.GetWarehousesAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateWarehouseAsync_CreatesWarehouse()
    {
        var context = CreateInMemoryDbContext();
        var service = new WarehouseService(context, null);
        var warehouseCreateDTO = new WarehouseCreateDTO { Name = "New Warehouse" };

        var result = await service.CreateWarehouseAsync(warehouseCreateDTO);

        Assert.NotNull(result);
        Assert.Equal("New Warehouse", result.Name);
        Assert.True(context.Warehouses.Any(w => w.Name == "New Warehouse"));
    }

    [Fact]
    public async Task ReplenishWarehouseAsync_IncreasesProductCount()
    {
        var context = CreateInMemoryDbContext();
        var warehouse = new Warehouse { Id = 1, Name = "Warehouse 1" };
        var product = new Product { Id = 1, Name = "Product 1", Price = 10.0 };
        var warehouseProduct = new WarehouseProduct { Id = 1, ProductId = 1, WareHouseId = 1, ProductCount = 10 };

        context.Warehouses.Add(warehouse);
        context.Products.Add(product);
        context.WarehouseProducts.Add(warehouseProduct);
        context.SaveChanges();

        var service = new WarehouseService(context, null);
        var adjustmentDTO = new WarehouseAdjustmentDTO { ProductId = 1, Count = 5 };

        await service.ReplenishWarehouseAsync(1, adjustmentDTO);

        var updatedWarehouseProduct = await context.WarehouseProducts.FindAsync(1);
        Assert.Equal(15, updatedWarehouseProduct.ProductCount);
    }

    [Fact]
    public async Task ShipFromWarehouseAsync_DecreasesProductCount()
    {
        var context = CreateInMemoryDbContext();
        var warehouse = new Warehouse { Id = 1, Name = "Warehouse 1" };
        var product = new Product { Id = 1, Name = "Product 1", Price = 10.0 };
        var warehouseProduct = new WarehouseProduct { Id = 1, ProductId = 1, WareHouseId = 1, ProductCount = 10 };

        context.Warehouses.Add(warehouse);
        context.Products.Add(product);
        context.WarehouseProducts.Add(warehouseProduct);
        context.SaveChanges();

        var service = new WarehouseService(context, null);
        var adjustmentDTO = new WarehouseAdjustmentDTO { ProductId = 1, Count = 5 };

        await service.ShipFromWarehouseAsync(1, adjustmentDTO);

        var updatedWarehouseProduct = await context.WarehouseProducts.FindAsync(1);
        Assert.Equal(5, updatedWarehouseProduct.ProductCount);
    }

    [Fact]
    public async Task GetWarehouseStatsAsync_ReturnsCorrectStats()
    {
        var context = CreateInMemoryDbContext();
        var warehouse = new Warehouse { Id = 1, Name = "Warehouse 1" };
        var product = new Product { Id = 1, Name = "Product 1", Price = 10.0 };

        var warehouseProduct = new WarehouseProduct { Id = 1, ProductId = 1, WareHouseId = 1, ProductCount = 10, Product = product };

        context.Warehouses.Add(warehouse);
        context.WarehouseProducts.Add(warehouseProduct);
        context.SaveChanges();

        var service = new WarehouseService(context, null);

        var result = await service.GetWarehouseStatsAsync(1);

        Assert.Equal(100.0, result.TotalPrice);
        Assert.Equal(10, result.TotalProducts);
    }
}
