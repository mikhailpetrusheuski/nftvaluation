using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nethereum.Contracts;
using Nethereum.Web3;
using TokenQueryService.Application.Commands;
using TokenQueryService.Dtos;

namespace TokenQueryService.Application.Handlers
{
    public class TokenQueryCommandHandler : IRequestHandler<TokenQueryCommand, TokenQueryResultDto>
    {
        private readonly Web3 _web3;

        public TokenQueryCommandHandler(Web3 web3)
        {
            _web3 = web3;
        }

        public async Task<TokenQueryResultDto> Handle(TokenQueryCommand request, CancellationToken cancellationToken)
        {
            // Prepare the function message
            var functionMessage = new FunctionMessage();

            // Get the contract
            var contract = _web3.Eth.GetContract("ContractABI", request.ContractAddress);

            // Get the tokenURI function
            var function = contract.GetFunction("tokenURI");

            //ToDo: Add retry logic using Polly.
            // Call the function and get the result
            var result = await function.CallAsync<string>(functionMessage);
            
            //Here need to publish the event to the message bus.

            return new TokenQueryResultDto(result);
        }
    }
}
