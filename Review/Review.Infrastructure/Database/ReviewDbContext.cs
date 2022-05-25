using Microsoft.EntityFrameworkCore;
using Review.Infrastructure.WriteModel.Database.Entities;

namespace Review.Infrastructure.WriteModel.Database
{
    internal class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ReviewRefresh> ReviewsRefresh { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
    }
}
