using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.WriteModel.Database.Entities
{
    internal class Like
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
    }
}
