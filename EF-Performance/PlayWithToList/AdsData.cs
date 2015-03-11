namespace PlayWithToList
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using AdsDatabase;

    class AdsData
    {
        static void Main()
        {

            // Variant 1. queryCount = 30
            var db = new AdsEntities();

            var ad = db.Ads.ToList();

            var starTime = DateTime.Now;

            var ads = db.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    a.Title,
                    a.Category,
                    a.Town,
                    a.Date
                })
                .ToList()
                .OrderBy(a => a.Date);

            var endTime = DateTime.Now;

            Console.WriteLine(endTime - starTime);
            Console.WriteLine(ads.Count());


            // Variant 2. queryCount = 1
            starTime = DateTime.Now;

            var ads2 = db.Ads
                .Where(a => a.AdStatus.Status == "Published")
                //.OrderBy(a => a.Date)
                .Select(a => new
                {
                    a.Title,
                    a.Category,
                    a.Town
                })
                .ToList();
            
            endTime = DateTime.Now;

            Console.WriteLine(endTime - starTime);
            Console.WriteLine(ads2.Count());

        }
    }
}
