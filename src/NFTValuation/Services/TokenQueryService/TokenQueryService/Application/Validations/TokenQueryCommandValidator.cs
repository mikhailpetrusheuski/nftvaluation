using Core.CQRS;
using FluentValidation;
using TokenQueryService.Application.Commands;

namespace TokenQueryService.Application.Validations
{
    public class TokenQueryCommandValidator : AbstractValidator<TokenQueryCommand>
    {
        public TokenQueryCommandValidator()
        {
            //ToDo: Add more validations rules here if needed.
            
            RuleFor(command => command.TokenId)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.CannotBeEmpty);

            RuleFor(command => command.ContractAddress)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.CannotBeEmpty);

            RuleFor(command => command.TokenIndex)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.CannotBeEmpty);
        }
    }
}