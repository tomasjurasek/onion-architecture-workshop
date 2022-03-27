using Microsoft.Extensions.DependencyInjection;
using Review.Application.Queries;
using Review.Domain.Services;
using Review.Infrastructure.Collections;
using Review.Infrastructure.Queries;

namespace Review.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IGetReviewsQuery, GetReviewQUery>();
            services.AddSingleton<IReviewCollection, ReviewCollection>();

            return services;
        }
    }
}
