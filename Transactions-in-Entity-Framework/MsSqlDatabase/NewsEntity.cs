using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MsSqlDatabase
{
    using System.Data.Entity;

    public class NewsEntity : DbContext
    {
        public NewsEntity() : base("NewsDatabase")
        {
        }

        public DbSet<PieceOfNews> News { get; set; }
    }
}
