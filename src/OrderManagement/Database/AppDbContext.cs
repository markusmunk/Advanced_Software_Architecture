using Microsoft.EntityFrameworkCore;
using OrderManagement.Database.Models;

namespace OrderManagement.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}