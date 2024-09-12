using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;

namespace NewStorageLab.Domain.Services;

public interface IProductService
{
    Task<Product> GetProductAsync(int id);
    Task<List<Product>> GetWarehouseProductsAsync(int warehouseId);
    Task<List<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(ProductCreationDTO productCreationDTO);
}
