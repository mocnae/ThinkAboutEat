using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Recipe.Application.Data;
using Recipe.Domain.Models;

namespace Recipe.Infrastructure.Data;

public class ApplicationDBContext : DbContext, IApplicationDbContext
{
    public DbSet<Recipe.Domain.Models.Recipe> Recipes { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<RecipeIngridient> RecipeIngridients { get; set; }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> contextOptions) : base(contextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
