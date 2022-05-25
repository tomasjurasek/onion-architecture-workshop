using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.WriteModel.Database.Entities
{
    internal class Review
    {
        [Key]
        public long Id { get; set; }
        public Guid ReviewId { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Dislike> Dislikes { get; set; } = new List<Dislike>();

    }
}
