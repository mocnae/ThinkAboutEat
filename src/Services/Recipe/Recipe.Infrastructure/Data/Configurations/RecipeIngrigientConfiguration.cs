using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Domain.Models;
using Recipe.Domain.ValueObjects;

namespace Recipe.Infrastructure.Data.Configurations;

public class RecipeIngrigientConfiguration : IEntityTypeConfiguration<RecipeIngridient>
{
    public void Configure(EntityTypeBuilder<RecipeIngridient> builder)
    {
        builder.HasKey(x => new { x.Id, x.RecipeId });
        
        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            dbId => RecipeIngridientId.Of(dbId)
        );

        builder.Property(x => x.RecipeId).HasConversion(
            id => id.Value,
            dbId => RecipeId.Of(dbId)
        );

        builder.Property(x => x.Gramm).IsRequired();

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        
    }
}
