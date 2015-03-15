namespace MsSqlDatabase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<NewsEntity>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NewsEntity context)
        {
            var db = new NewsEntity();

            var news = new List<PieceOfNews>
            {
                new PieceOfNews
                {
                    Content = @"Problem 1.	News Database (Code First)"
                },
                new PieceOfNews
                {
                    Content = "Problem 2. Updates (Console App)"
                },
                new PieceOfNews
                {
                    Content = "Problem 3.  * Concurrent Updates (GUI App)"
                },
                new PieceOfNews
                {
                    Content = "Problem 4. ATM Database"
                },
                new PieceOfNews
                {
                    Content = "Problem 5.  Transactional ATM Withdrawal"
                },

            };

            

            news.ForEach(delegate(PieceOfNews n)
            {
                if (!db.News.Any(x => n.Content == x.Content))
                {
                    db.News.AddOrUpdate(n);
                }

            });

            db.SaveChanges();

        }
    }
}
