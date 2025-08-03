using Microsoft.EntityFrameworkCore;

namespace BookingGuru.Common.Infrastructure.Repositories;

public interface IModuleDbContextBuilder
{
    void Configure(ModelBuilder modelBuilder);
}