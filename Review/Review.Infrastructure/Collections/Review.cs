using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.Collections
{
    public  class Review : TableEntity
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
    }
}
