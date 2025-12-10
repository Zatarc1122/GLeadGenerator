using FluentValidation;
using FluentValidation.Validators;

namespace GLeadGenerator.Contract.Users;

public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestDtoValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithErrorCode("User.FirstNameRequired")
            .MaximumLength(100).WithErrorCode("User.FirstNameMaxLength");

        RuleFor(user => user.LastName)
            .NotEmpty().WithErrorCode("User.LastNameRequired")
            .MaximumLength(100).WithErrorCode("User.LastNameMaxLength");

        RuleFor(user => user.Email)
            .NotEmpty().WithErrorCode("User.EmailRequired")
            .MaximumLength(320).WithErrorCode("User.EmailMaxLength")
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithErrorCode("User.EmailFormat");
    }
}

