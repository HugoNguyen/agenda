using FluentValidation;

namespace BookingGuru.Modules.Mocks.Application.PublishClones.CreatePublishClone;

internal sealed class CreatePublishCloneCommandValidator : AbstractValidator<CreatePublishCloneCommand>
{
    public CreatePublishCloneCommandValidator()
    {
        RuleFor(c => c.PublishId).NotEmpty();
        RuleFor(c => c.Name).NotNull().MaximumLength(200);
        RuleFor(c => c.PublishDateUtc).NotNull();
    }
}