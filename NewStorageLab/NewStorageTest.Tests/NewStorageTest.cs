using Microsoft.EntityFrameworkCore;
using NewStorageLab.DAL;
using NewStorageLab.DAL.Models;
using System;
using System.Linq;
using Xunit;

namespace NewStorageLab.Tests
{
    public class AppDbContextTests
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContextTests()
        {
            {

                _options = new DbContextOptionsBuilder<AppDbContext>()
                              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                              .Options;
            }
        }

        [Fact]
        public void AddProduct_ShouldAddProductToDatabase()
        {
            // Arrange
            using var context = new AppDbContext(_options);
            var product = new Product { Id = 1, Name = "Test Product" };

            // Act
            context.Products.Add(product);
            context.SaveChanges();

            // Assert
            var retrievedProduct = context.Products.SingleOrDefault(p => p.Id == 1);
            Assert.NotNull(retrievedProduct);
            Assert.Equal("Test Product", retrievedProduct.Name);
        }

        [Fact]
        public void AddWarehouse_ShouldAddWarehouseToDatabase()
        {
            // Arrange
            using var context = new AppDbContext(_options);
            var warehouse = new Warehouse { Id = 1, Name = "Test Warehouse" };

            // Act
            context.Warehouses.Add(warehouse);
            context.SaveChanges();

            // Assert
            var retrievedWarehouse = context.Warehouses.SingleOrDefault(w => w.Id == 1);
            Assert.NotNull(retrievedWarehouse);
            Assert.Equal("Test Warehouse", retrievedWarehouse.Name);
        }

        [Fact]
        public void AddWarehouseProduct_ShouldAddWarehouseProductToDatabase()
        {
            // Arrange
            using var context = new AppDbContext(_options);
            var product = new Product { Id = 1, Name = "Test Product" };
            var warehouse = new Warehouse { Id = 1, Name = "Test Warehouse" };
            context.Products.Add(product);
            context.Warehouses.Add(warehouse);
            context.SaveChanges();

            var warehouseProduct = new WarehouseProduct
            {
                Id = 1,
                ProductId = product.Id,
                WareHouseId = warehouse.Id,
                ProductCount = 10
            };

            // Act
            context.WarehouseProducts.Add(warehouseProduct);
            context.SaveChanges();

            // Assert
            var retrievedWarehouseProduct = context.WarehouseProducts.SingleOrDefault(wp => wp.Id == 1);
            Assert.NotNull(retrievedWarehouseProduct);
            Assert.Equal(10, retrievedWarehouseProduct.ProductCount);
        }
    }
}