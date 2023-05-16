using MediatR;
using TokenQueryService.Dtos;

namespace TokenQueryService.Application.Commands
{
    public record TokenQueryCommand(string TokenId, string TokenIndex, string ContractAddress) : IRequest<TokenQueryResultDto>;
}
