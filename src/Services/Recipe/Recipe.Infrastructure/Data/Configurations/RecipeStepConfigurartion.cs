using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Domain.Models;
using Recipe.Domain.ValueObjects;

namespace Recipe.Infrastructure.Data.Configurations;

public class RecipeStepConfigurartion : IEntityTypeConfiguration<RecipeStep>
{
    public void Configure(EntityTypeBuilder<RecipeStep> builder)
    {
        builder.HasKey(x =>
            new { x.Id, x.RecipeId });

        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            dbId => RecipeStepId.Of(dbId));

        builder.Property(x => x.RecipeId).HasConversion(
            id => id.Value,
            dbId => RecipeId.Of(dbId));

        builder.Property(x => x.PhotoPath).HasMaxLength(255);

        builder.Property(x => x.Name).HasMaxLength(255);
    }
}
