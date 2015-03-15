using System.Collections.Generic;

namespace AtmDatabase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    public sealed class Configuration : DbMigrationsConfiguration<AtmDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AtmDbContext context)
        {
            var db = new AtmDbContext();

            var accounts = new List<Account>
            {
                new Account
                {
                    CardNumber = "1234567890",
                    CardPIN = "0000",
                    CardCash = 1500
                },
                new Account
                {
                    CardNumber = "a123456789",
                    CardPIN = "1111",
                    CardCash = 1500
                },
                new Account
                {
                    CardNumber = "ab12345678",
                    CardPIN = "0101",
                    CardCash = 1500
                }
            };

            accounts.ForEach(delegate(Account a)
            {
                if (!db.Accounts.Any(x => a.CardNumber == x.CardNumber))
                {
                    db.Accounts.AddOrUpdate(a);
                }
            });

            db.SaveChanges();
        }
    }
}
