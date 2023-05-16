using Core.CQRS;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingBehavior(this IServiceCollection services)
            => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        public static IServiceCollection AddValidatorBehavior(this IServiceCollection services)
            => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }
}