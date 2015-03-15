namespace AtmDatabase
{
    using System.Data.Entity;

    public class AtmDbContext : DbContext
    {
        public AtmDbContext() :base("name=AtmDatabase")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
    }
}
