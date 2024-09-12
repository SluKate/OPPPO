using Microsoft.AspNetCore.Mvc;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;

namespace NewStorageLab.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Warehouse>> GetWarehouseAsync(int id)
    {
        return Ok();
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Warehouse>>> GetWarehousesAsync()
    {
        return Ok();
    }

    [HttpGet("product-warehouses")]
    public async Task<ActionResult<List<Warehouse>>> GetProductWarehousesAsync()
    {
        return Ok();
    }

    [HttpGet("warehouse-stats")]
    public async Task<ActionResult<WarehouseStatsDTO>> GetWareHouseStatsAsync(int warehauseId)
    {
        return Ok();
    }

    [HttpPost("create")]
    public async Task<ActionResult<Warehouse>> CreateWarehouseAsync([FromBody] WarehouseCreateDTO warehouseCreateDTO)
    {
        return Ok();
    }

    [HttpPost("replenish/{warehouseId}")]
    public async Task<ActionResult<Warehouse>> ReplenishWarehouseAsync(int warehouseId, [FromBody] ProductReplanishDTO[] deliveryDTOs)
    {
        return Ok();
    }

    [HttpPost("ship/{warehouseId}")]
    public async Task<ActionResult<Warehouse>> ShipFromWarehouseAsync(int warehouseId, [FromBody] ProductReplanishDTO[] deliveryDTOs)
    {
        return Ok();
    }
}
