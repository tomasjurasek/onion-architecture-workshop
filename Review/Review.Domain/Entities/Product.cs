using Dawn;

namespace Review.Domain.Entities
{
    internal record Product
    {
        public string Code { get; }

        public Product(string code)
        {
            Code = Guard.Argument(code).NotNull().NotEmpty();
        }
    }
}
