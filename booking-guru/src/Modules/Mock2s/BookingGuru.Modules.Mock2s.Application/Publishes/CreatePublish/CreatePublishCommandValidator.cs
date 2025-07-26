using FluentValidation;

namespace BookingGuru.Modules.Mock2s.Application.Publishes.CreatePublish;

internal sealed class CreatePublishCommandValidator : AbstractValidator<CreatePublishCommand>
{
    public CreatePublishCommandValidator()
    {
        RuleFor(c => c.Name).NotNull().MaximumLength(200);
        RuleFor(c => c.PublishDateUtc).NotNull();
    }
}