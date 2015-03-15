using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using Microsoft.SqlServer.Server;

namespace Updates
{
    using System.Data.Entity;
    using System.Linq;
    using MsSqlDatabase;
    using MsSqlDatabase.Migrations;

    class MainClass
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsEntity, Configuration>());

            var firstUserContext = new NewsEntity();
            var secondUserContext = new NewsEntity();
            var lastId = firstUserContext.News.Count();
            var end = true;
            var isFirstSave = false;


            Console.WriteLine("First user");
            PrintNewsWithId(lastId);
            Console.Write("Enter the new content: ");
            var newContent = Console.ReadLine();
            UpdateNewsById(firstUserContext, lastId, newContent);
            Console.WriteLine("Changes successfully saved in the DB.");

            Console.WriteLine();

            Console.WriteLine("Second user");
            PrintNewsWithId(lastId);
 
            do
            {


                Console.Write("Enter the new content: ");
                newContent = Console.ReadLine();
                UpdateNewsById(secondUserContext, lastId, newContent);

                if (!isFirstSave)
                {
                    firstUserContext.SaveChanges();
                    isFirstSave = true;
                }
 
                try
                {
                    secondUserContext.SaveChanges();
                    end = false;
                    Console.WriteLine("Changes successfully saved in the DB.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.Write("Conflict! Content from DB: ");
                    PrintNewsWithId(lastId);

                    end = true;

                    firstUserContext.Dispose();
                    secondUserContext.Dispose();

                    secondUserContext = new NewsEntity();
                }
            } while (end);
        }

        static void UpdateNewsById(NewsEntity dbContext, int newsId, string newContent)
        {
            var updatedNews = dbContext.News.Find(newsId);
            updatedNews.Content = newContent;
        }

        static void PrintNewsWithId(int newsId)
        {
            var contex = new NewsEntity();
            var newsContent = contex.News.Find(newsId).Content;
            Console.WriteLine(newsContent);
        }
    }
}
