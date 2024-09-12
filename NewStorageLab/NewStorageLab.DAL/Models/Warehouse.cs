using System.ComponentModel.DataAnnotations;

namespace NewStorageLab.DAL.Models;

public class Warehouse
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
