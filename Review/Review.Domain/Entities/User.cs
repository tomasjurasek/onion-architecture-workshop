using Dawn;

namespace Review.Domain.Entities
{
    internal record User
    {
        public User(string userName)
        {
            UserName = Guard.Argument(userName).NotNull().NotEmpty();
        }

        public string UserName { get; }
    }
}
