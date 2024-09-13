using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStorageLab.Domain.DTOs;

public class ProductCreationDTO
{
    public string ProductName { get; set; } = null!;

    public double Price { get; set; }
}
