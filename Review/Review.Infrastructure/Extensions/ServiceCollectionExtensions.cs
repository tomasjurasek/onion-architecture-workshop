using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.Contracts;
using Review.Domain.Contracts;
using Review.Infrastructure;
using Review.Infrastructure.WriteModel;
using Review.Infrastructure.WriteModel.Database;

namespace Review.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddSingleton<IMetricCollector, MetricCollector>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddDbContextFactory<ReviewDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbContext")));
            return services;
        }
    }
}
