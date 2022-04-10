using Microsoft.Extensions.DependencyInjection;
using Review.Application.Extensions;
using Review.Infastructure.Extensions;

namespace Review.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddApplication()
                           .AddInfrastructure();

        }
    }
}