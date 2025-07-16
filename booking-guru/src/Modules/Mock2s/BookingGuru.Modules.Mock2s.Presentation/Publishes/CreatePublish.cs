using BookingGuru.Common.Domain;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Common.Presentation.Results;
using BookingGuru.Modules.Mock2s.Application.Publishes.CreatePublish;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BookingGuru.Modules.Mock2s.Presentation.Publishes;

internal sealed class CreatePublish : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("first-entities", async ([FromBody] Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreatePublishCommand(request.Name, request.Publish));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Publishes);
    }

    internal sealed class Request
    {
        public string Name { get; init; }

        public DateTime Publish { get; init; }
    }
}
