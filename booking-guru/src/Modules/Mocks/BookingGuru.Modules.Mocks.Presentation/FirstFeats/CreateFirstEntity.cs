using BookingGuru.Common.Domain;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Common.Presentation.Results;
using BookingGuru.Modules.Mocks.Application.FirstFeats.CreateFirstEntity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BookingGuru.Modules.Mocks.Presentation.FirstFeats;

internal sealed class CreateFirstEntity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("first-entities", async ([FromBody] Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateFirstEntityCommand(request.Field1, request.Field1Nullable, request.Field2));
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.FirstEntities);
    }

    internal sealed class Request
    {
        public string Field1 { get; init; }

        public string? Field1Nullable { get; init; }

        public DateTime? Field2 { get; init; }
    }
}
