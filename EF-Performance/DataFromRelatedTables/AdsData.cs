
namespace DataFromRelatedTables
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using AdsDatabase;

    class AdsData
    {
        static void Main()
        {
            var db = new AdsEntities();

            //// Variant 1. CountOfQueries = 1
            //var ads = db.Ads.Select(a => new
            //{
            //    a.Title,
            //    a.AdStatus,
            //    a.Category,
            //    a.Town,
            //    a.AspNetUser
            //});

            //foreach (var ad in ads)
            //{
            //    Console.WriteLine("{0} - {1} \n Category: {2}; Town: {3} \n User: {4}\n",
            //        ad.Title, ad.AdStatus, ad.Category, ad.Town, ad.AspNetUser);
            //}

            // Variant 2. CountOfQueries = 30
            foreach (var ad in db.Ads)
            {
                Console.WriteLine("{0} - {1} \n Category: {2}; Town: {3} \n User: {4}\n",
                    ad.Title, ad.AdStatus, ad.Category, ad.Town, ad.AspNetUser);
            }

            //// Variant 3. CountOfQueries = 1
            //foreach (var ad in db.Ads
            //    .Include(a => a.AdStatus)
            //    .Include(a => a.Town)
            //    .Include(a => a.Category)
            //    .Include(a => a.AspNetUser))
            //{
            //    Console.WriteLine("{0} - {1} \n Category: {2}; Town: {3} \n User: {4}\n",
            //        ad.Title, ad.AdStatus, ad.Category, ad.Town, ad.AspNetUser);
            //}
        }
    }
}
