using ECommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApi.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options) { }
        

        public  DbSet<Product> Products { get; set; }

        //public virtual DbSet<User> Users { get; set; }
    }
}
