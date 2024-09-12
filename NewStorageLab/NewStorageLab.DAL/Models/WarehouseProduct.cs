using System.ComponentModel.DataAnnotations;

namespace NewStorageLab.DAL.Models;

public class WarehouseProduct
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; } = 0;
    public Product? Product { get; set; }

    [Required]
    public int WareHouseId { get; set; } = 0;
    public Warehouse? Warehouse { get; set; }

    public int ProductCount { get; set; }
}
