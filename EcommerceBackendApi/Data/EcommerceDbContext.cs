using EcommerceBackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackendApi.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions options) : base(options)
        {
        }
           public DbSet<Product> Products { get; set; }
           public DbSet<User> Users { get; set; }
           public DbSet<Store> Stores { get; set; }
    }
}
