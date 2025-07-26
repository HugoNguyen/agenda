using BookingGuru.Common.Domain;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Common.Presentation.Results;
using BookingGuru.Modules.Mocks.Application.Publishes.GetPublishClone;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BookingGuru.Modules.Mocks.Presentation.PublishClones;

internal sealed class GetPublishClone : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("first-entities/{id}", async (Guid id, ISender sender) =>
        {
            Result<PublishCloneResponse> result = await sender.Send(new GetPublishCloneQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.FirstEntities);
    }
}