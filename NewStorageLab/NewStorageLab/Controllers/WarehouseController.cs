using Microsoft.AspNetCore.Mvc;
using NewStorageLab.DAL.Models;
using NewStorageLab.Domain.DTOs;
using NewStorageLab.Domain.Services;
using TaskTracker.Headers;

namespace NewStorageLab.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidationExceptionFilter]

public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Warehouse>> GetWarehouseAsync(int id)
    {
        var res = await _warehouseService.GetWarehouseAsync(id);
        return Ok(res);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Warehouse>>> GetWarehousesAsync()
    {
        var res = await _warehouseService.GetWarehousesAsync();
        return Ok(res);
    }

    [HttpGet("warehouse-stats")]
    public async Task<ActionResult<WarehouseStatsDTO>> GetWareHouseStatsAsync(int warehauseId)
    {
        var res = await _warehouseService.GetWarehouseStatsAsync(warehauseId);
        return Ok(res);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Warehouse>> CreateWarehouseAsync([FromBody] WarehouseCreateDTO warehouseCreateDTO)
    {
        var res = await _warehouseService.CreateWarehouseAsync(warehouseCreateDTO);
        return Ok(res);
    }

    [HttpPatch("replenish/{warehouseId}")]
    public async Task<ActionResult> ReplenishWarehouseAsync(int warehouseId, [FromBody] WarehouseAdjustmentDTO deliveryDTOs)
    {
        await _warehouseService.ReplenishWarehouseAsync(warehouseId, deliveryDTOs);
        return Ok();
    }

    [HttpPatch("ship/{warehouseId}")]
    public async Task<ActionResult> ShipFromWarehouseAsync(int warehouseId, [FromBody] WarehouseAdjustmentDTO shippingDTOs)
    {
        await _warehouseService.ShipFromWarehouseAsync(warehouseId, shippingDTOs);
        return Ok();
    }
}
