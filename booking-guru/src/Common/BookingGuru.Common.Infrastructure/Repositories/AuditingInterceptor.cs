using BookingGuru.Common.Application.Timing;
using BookingGuru.Common.Domain.Entities.Auditing;
using BookingGuru.Common.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookingGuru.Common.Infrastructure.Repositories;

public sealed class AuditingInterceptor : SaveChangesInterceptor
{
    private readonly IClock _clock;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditingInterceptor(IClock clock, IHttpContextAccessor httpContextAccessor)
    {
        _clock = clock;
        _httpContextAccessor = httpContextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = context.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            var now = _clock.Now;
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            if (entry.State == EntityState.Added && entry.Entity is IHasCreationTime auditable)
            {
                auditable.CreationTime = now;
            }

            if (entry.State == EntityState.Modified && entry.Entity is IHasModificationTime auditableModified)
            {
                auditableModified.LastModificationTime = now;
            }

            if (entry.State == EntityState.Deleted && entry.Entity is IHasDeletionTime softDeletable)
            {
                entry.State = EntityState.Modified;
                softDeletable.DeletionTime = now;
                softDeletable.IsDeleted = true;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}