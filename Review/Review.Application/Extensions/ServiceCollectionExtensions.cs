using Microsoft.Extensions.DependencyInjection;
using Review.Application.Services;

namespace Review.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IReviewService, ReviewService>();

            return services;
        }
    }
}
