namespace Review.Application.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime Utc { get; }
    }
}
