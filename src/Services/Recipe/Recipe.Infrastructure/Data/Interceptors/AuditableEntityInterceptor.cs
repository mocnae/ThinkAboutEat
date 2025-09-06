using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Recipe.Domain.Abstractions;

namespace Recipe.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        AuditEntity(eventData.Context);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AuditEntity(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditEntity(DbContext context)
    {
        var entities = context.ChangeTracker.Entries<IEntity>();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedAt = DateTime.UtcNow;
                entity.Entity.CreatedBy = "Andrew";
            }

            if (entity.State == EntityState.Modified || entity.State == EntityState.Added || entity.HasChangedOwnedEntities())
            {
                entity.Entity.LastModified = DateTime.UtcNow;
                entity.Entity.LastModifiedBy = "Andrew";
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(
            x => x.TargetEntry != null && x.TargetEntry.Metadata.IsOwned() && (x.EntityEntry.State == EntityState.Added || x.EntityEntry.State == EntityState.Modified)
        );
    }
}
