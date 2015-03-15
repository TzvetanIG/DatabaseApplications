

namespace MsSqlDatabase
{
    using System;
    using Migrations;
    using System.Data.Entity;
    using System.Linq;
    
    class MainClass
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsEntity, Configuration>());
            var db = new NewsEntity();

            var news = db.News.ToList();

            news.ForEach(n => Console.WriteLine(n.Content));
        }
    }
}
