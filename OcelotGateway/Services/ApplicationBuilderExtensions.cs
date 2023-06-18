using OcelotGateway.Helpers;

namespace OcelotGateway.Services
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)=>
            applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
    }
}
