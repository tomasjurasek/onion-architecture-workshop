using Review.Application.Contracts;

namespace Review.Infrastructure
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Utc => DateTime.UtcNow;
    }
}
