using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using product_api.Models;

namespace product_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    State = "Draft",
                    Title = "Milk",
                    Image_url = "https...",
                    Price = 200,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Description = "Description"
                },
                new Product
                {
                    Id = 2,
                    State = "Draft",
                    Title = "Flower",
                    Image_url = "https...",
                    Price = 500,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Description = "Description"
                },
                new Product
                {
                    Id = 3,
                    State = "Draft",
                    Title = "Fish",
                    Image_url = "https...",
                    Price = 100,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Description = "Description"
                },
                new Product
                {
                    Id = 4,
                    State = "Draft",
                    Title = "Wood",
                    Image_url = "https...",
                    Price = 270,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Description = "Description"
                });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User() { Name = "jason_admin", Email = "jason.admin@email.com", Password = "MyPass_w0rd", Role = "Admin" },
            new User() { Name = "elyse_manager", Email = "elyse.seller@email.com", Password = "MyPass_w0rd", Role = "Manager" },
            new User() { Name = "elyse_customer", Email = "elyse.customer@email.com", Password = "MyPass_w0rd", Role = "Customer" });
        }

    }
}
