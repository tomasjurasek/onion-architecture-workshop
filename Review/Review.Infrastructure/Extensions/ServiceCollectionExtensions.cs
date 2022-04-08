using Microsoft.Extensions.DependencyInjection;
using Review.Application.Queries;
using Review.Domain.Services;
using Review.Infrastructure;
using Review.Infrastructure.Queries;
using Review.Infrastructure.Repositories;

namespace Review.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IGetReviewsQuery, GetReviewQUery>();
            services.AddSingleton<IReviewRepositories, ReviewRepository>();
            services.AddHostedService<ReviewRefresher>();

            return services;
        }
    }
}
