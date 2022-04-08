using Microsoft.Extensions.Hosting;

namespace Review.Infrastructure
{
    internal class ReviewRefresher : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
