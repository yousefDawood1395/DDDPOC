using OcelotGateway.Configuration;

namespace OcelotGateway.Services;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCorrelationIdManager(this IServiceCollection services)
    {
        services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        return services;
    }
}
