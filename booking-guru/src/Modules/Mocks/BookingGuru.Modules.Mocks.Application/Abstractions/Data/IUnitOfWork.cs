﻿using System.Data.Common;

namespace BookingGuru.Modules.Mocks.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
