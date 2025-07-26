using FluentValidation;

namespace BookingGuru.Modules.Mocks.Application.FirstFeats.CreateFirstEntity;

internal sealed class CreateFirstEntityCommandValidator : AbstractValidator<CreateFirstEntityCommand>
{
    public CreateFirstEntityCommandValidator()
    {
        RuleFor(c => c.Field1).NotNull().MaximumLength(200).MinimumLength(1);
        RuleFor(c => c.Field1Nullable).MaximumLength(200);
    }
}
