using BookingGuru.Common.Domain;

namespace BookingGuru.Common.Application.Exceptions;

public sealed class BookingGuruException : Exception
{
    public BookingGuruException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
