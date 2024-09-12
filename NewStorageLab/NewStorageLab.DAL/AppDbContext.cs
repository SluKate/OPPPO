using Microsoft.EntityFrameworkCore;
using NewStorageLab.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStorageLab.DAL;

public class AppDbContext: DbContext
{
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<WarehouseProduct> WarehouseProducts { get; set; } = null!;

    public AppDbContext(DbContextOptions options): base(options)
    {
        Database.EnsureCreated();
    }
}
