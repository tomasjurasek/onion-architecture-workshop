using Microsoft.Extensions.DependencyInjection;
using Review.Application.Extensions;

namespace Review.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddApplication()
                           .AddInfrastructure();
        }
    }
}