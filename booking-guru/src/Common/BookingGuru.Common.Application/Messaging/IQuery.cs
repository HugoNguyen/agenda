using BookingGuru.Common.Domain;
using MediatR;

namespace BookingGuru.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
