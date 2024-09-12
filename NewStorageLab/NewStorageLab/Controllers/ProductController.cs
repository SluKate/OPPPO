using Microsoft.AspNetCore.Mvc;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;
using NewStorageLab.Domain.Services;

namespace NewStorageLab.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        var res = await _productService.GetProductAsync(id);

        return Ok(res);
    }

    [HttpGet("warehouse-products")]
    public async Task<ActionResult<List<Product>>> GetWarehouseProductsAsync(int warehouseId)
    {
        var res = await _productService.GetWarehouseProductsAsync(warehouseId);

        return Ok(res);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Product>>> GetProductsAsync()
    {
        var res = await _productService.GetProductsAsync();

        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync([FromBody] ProductCreationDTO productCreationDTO)
    {
        var res = await _productService.CreateProductAsync(productCreationDTO);

        return Ok(res);
    }
}
