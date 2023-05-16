using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.CQRS
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var commandName = request.GetGenericTypeName();

            _logger.LogInformation("----- Handling command {CommandName}", commandName);
            _logger.LogDebug("----- Handling command request body: {Command}", request);

            var response = await next().ConfigureAwait(false);

            _logger.LogInformation("----- Command {CommandName} handled", commandName);
            _logger.LogDebug("----- Command {CommandName} handled, response:  {@Response}", commandName, response);

            return response;
        }
    }
}