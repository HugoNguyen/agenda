using BookingGuru.Common.Domain;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Common.Presentation.Results;
using BookingGuru.Modules.Mocks.Application.FirstFeats.GetFirstEntity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BookingGuru.Modules.Mocks.Presentation.FirstFeats;

internal sealed class GetFirstEntity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("first-entities/{id}", async (Guid id, ISender sender) =>
        {
            Result<FirstEntityResponse> result = await sender.Send(new GetFirstEntityQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.FirstEntities);
    }
}