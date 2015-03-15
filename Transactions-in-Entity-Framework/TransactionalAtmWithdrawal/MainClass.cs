namespace TransactionalAtmWithdrawal
{
    using System;
    using System.Data.Entity;
    using AtmDatabase;
    using AtmDatabase.Migrations;

    class MainClass
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AtmDbContext, Configuration>());
            var db = new AtmDbContext();


            //using (var tran = db.Database.BeginTransaction())
            //{
                try
                {
                    Console.Write("Enter card number: ");
                    var cardNumber = Console.ReadLine();

                    Console.Write("Enter PIN: ");
                    var pin = Console.ReadLine();

                    var account = AtmManager.GetAccount(db, cardNumber, pin);

                    Console.Write("Enter amount: ");
                    var amount = decimal.Parse(Console.ReadLine());

                    AtmManager.CheckAmount(account, amount);

                    account.CardCash -= amount;
                    
                    db.SaveChanges();
                    //tran.Commit();

                    var history = new TransactionHistory
                    {
                        CardNumber = cardNumber,
                        TransactionDate = DateTime.Now,
                        Amount = amount
                    };

                    db.TransactionHistory.Add(history);

                    db.SaveChanges();

                    Console.WriteLine("Take your money: {0}", amount);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //tran.Rollback();
                }
            //}

        }
    }
}
