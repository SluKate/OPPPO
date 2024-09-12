using Microsoft.EntityFrameworkCore;
using NewStorageLab.DAL;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;
using NewStorageLab.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStorageLab.PoductService;

public class ProductService: IProductService
{
    private readonly AppDbContext _appDbContext;

    public ProductService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Product>> GetWarehouseProductsAsync(int warehouseId)
    {
        var warehouse = await _appDbContext.Warehouses
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == warehouseId);

        var products = warehouse?.Products;

        return products?.ToList();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _appDbContext.Products.ToListAsync();
    }

    public async Task<Product> CreateProductAsync(ProductCreationDTO productCreationDTO)
    {
        var product = new Product()
        {
            Name = productCreationDTO.ProductName,
            Price = productCreationDTO.Price,
        };

         await _appDbContext.AddAsync(product);
         await _appDbContext.SaveChangesAsync();

        return product;
    }
}
