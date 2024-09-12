using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStorageLab.DAL.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public double Price { get; set; }

    public ICollection<Warehouse> GetWarehouses { get; set; } = new List<Warehouse>();
}
