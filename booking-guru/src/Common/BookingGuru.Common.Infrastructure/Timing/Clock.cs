using BookingGuru.Common.Application.Timing;
using Microsoft.Extensions.Options;

namespace BookingGuru.Common.Infrastructure.Timing;

public class Clock : IClock
{
    protected ClockOptions Options { get; }

    public Clock(
        IOptions<ClockOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        Options = options.Value;
    }

    public virtual DateTime Now => Options.Kind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;

    public virtual DateTimeKind Kind => Options.Kind;

    public virtual bool SupportsMultipleTimezone => Options.Kind == DateTimeKind.Utc;

    /// <summary>
    /// Normalizes given <see cref="DateTime"/>.
    /// </summary>
    /// <param name="dateTime">DateTime to be normalized.</param>
    /// <returns>Normalized DateTime</returns>
    public virtual DateTime Normalize(DateTime dateTime)
    {
        if (Kind == DateTimeKind.Unspecified || Kind == dateTime.Kind)
        {
            return dateTime;
        }

        if (Kind == DateTimeKind.Local && dateTime.Kind == DateTimeKind.Utc)
        {
            return dateTime.ToLocalTime();
        }

        if (Kind == DateTimeKind.Utc && dateTime.Kind == DateTimeKind.Local)
        {
            return dateTime.ToUniversalTime();
        }

        return DateTime.SpecifyKind(dateTime, Kind);
    }
}