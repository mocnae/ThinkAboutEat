using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Domain.Models;
using Recipe.Domain.ValueObjects;

namespace Recipe.Infrastructure.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe.Domain.Models.Recipe>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Recipe> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            dbId => RecipeId.Of(dbId)
        );

        builder.HasMany(x => x.RecipeSteps)
            .WithOne()
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.RecipeIngridients)
            .WithOne()
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(500);
    }
}
