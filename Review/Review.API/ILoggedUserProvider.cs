namespace Review.API
{
    public interface ILoggedUserProvider
    {
        Guid UserId { get; }
    }
}
