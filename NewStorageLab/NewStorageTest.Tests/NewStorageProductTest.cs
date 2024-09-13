using Microsoft.EntityFrameworkCore;
using NewStorageLab.DAL;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;
using NewStorageLab.PoductService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ProductServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppDbContext(options);
        _productService = new ProductService(_dbContext);

        SeedData();
    }

    private void SeedData()
    {
        if (!_dbContext.Products.Any())
        {
            _dbContext.Products.AddRange(
                new Product { Id = 1, Name = "Product1", Price = 10.0 },
                new Product { Id = 2, Name = "Product2", Price = 20.0 }
            );

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public async Task GetProductAsync_ValidId_ReturnsProduct()
    {
        var result = await _productService.GetProductAsync(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Product1", result.Name);
    }

    [Fact]
    public async Task GetProductAsync_InvalidId_ReturnsNull()
    {
        var result = await _productService.GetProductAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsAllProducts()
    {
        var result = await _productService.GetProductsAsync();
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateProductAsync_ValidDTO_AddsProduct()
    {
        var productDTO = new ProductCreationDTO
        {
            ProductName = "Product3",
            Price = 30.0
        };

        var result = await _productService.CreateProductAsync(productDTO);
        Assert.NotNull(result);
        Assert.Equal("Product3", result.Name);
        Assert.Equal(30.0, result.Price);
    }
}
