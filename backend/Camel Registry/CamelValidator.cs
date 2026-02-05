using FluentValidation;

public class CamelValidator : AbstractValidator<Camel>
{
    public CamelValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(c => c.HumpCount)
            .InclusiveBetween(1, 2)
            .WithMessage("HumpCount must be 1 or 2.");

        RuleFor(c => c.LastFed)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("LastFed cannot be in the future.");
    }
}
