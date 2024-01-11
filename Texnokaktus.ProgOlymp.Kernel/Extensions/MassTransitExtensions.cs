using MassTransit;

namespace Texnokaktus.ProgOlymp.Kernel.Extensions;

internal static class MassTransitExtensions
{
    public static IConsumerConfigurator<TConsumer> Retry<TConsumer>(this IConsumerConfigurator<TConsumer> consumerConfigurator,
                                                                    int retryLimit,
                                                                    int intervalSeconds,
                                                                    int incrementSeconds) where TConsumer : class
    {
        consumerConfigurator.UseMessageRetry(retryConfigurator => retryConfigurator.Incremental(retryLimit, 
                                                                                                TimeSpan.FromSeconds(intervalSeconds),
                                                                                                TimeSpan.FromSeconds(incrementSeconds)));
        return consumerConfigurator;
    }
}
