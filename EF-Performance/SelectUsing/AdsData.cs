using System.Linq;

namespace SelectUsing
{
    using System;
    using System.Data.Entity;
    using AdsDatabase;

    class AdsData
    {
        static void Main()
        {

            var db = new AdsEntities();
            var ads = db.Ads.ToList();

            // Variant 1. 
            var starTime = DateTime.Now; 
            var ads1 = db.Ads
                .First();

            var endTime = DateTime.Now;

            Console.WriteLine(endTime - starTime);

            // Variant 2. 
            starTime = DateTime.Now; 
            
            var ads2 = db.Ads
                .Select(a => a.Title)
                .First();

            endTime = DateTime.Now;

            Console.WriteLine(endTime - starTime);

        }
    }
}
