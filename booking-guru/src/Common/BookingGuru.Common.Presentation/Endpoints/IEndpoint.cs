using Microsoft.AspNetCore.Routing;

namespace BookingGuru.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
