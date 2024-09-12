using Microsoft.AspNetCore.Mvc;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;

namespace NewStorageLab.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        return Ok();
    }

    [HttpGet("warehouse-products")]
    public async Task<ActionResult<List<Product>>> GetWarehouseProductsAsync(int warehouseId)
    {
        return Ok();
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Product>>> GetProductsAsync()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync([FromBody] ProductCreationDTO productCreationDTO)
    {
        return Ok();
    }
}
