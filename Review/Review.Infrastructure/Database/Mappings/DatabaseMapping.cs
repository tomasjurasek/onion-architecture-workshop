using Review.Infrastructure.WriteModel.Database.Entities;

namespace Review.Infrastructure.WriteModel.Database.Mappings
{
    internal static partial class DatabaseMapping
    {
        public static User ToDatabase(this Domain.Entities.User user)
        {
            return new User { UserName = user.UserName };
        }

        public static Product ToDatabase(this Domain.Entities.Product product)
        {
            return new Product { Code = product.Code };
        }

        public static Entities.Review ToDatabase(this Domain.Entities.Review review)
        {
            return new Entities.Review
            {
                ReviewId = review.Id,
                CreatedAt = review.CreatedAt,
                Product = review.Product.ToDatabase(),
                Description = review.Description,
                User = review.User.ToDatabase()
            };
        }
    }
}
