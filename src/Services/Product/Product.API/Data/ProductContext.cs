using System;
using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data;

public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => new { p.Id, p.Name });

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name).IsUnique();
    }
}
