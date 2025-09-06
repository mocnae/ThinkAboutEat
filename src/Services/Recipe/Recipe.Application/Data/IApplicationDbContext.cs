using System;
using Microsoft.EntityFrameworkCore;
using Recipe.Domain.Models;

namespace Recipe.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<Recipe.Domain.Models.Recipe> Recipes { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<RecipeIngridient> RecipeIngridients { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
