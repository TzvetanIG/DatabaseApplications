namespace TransactionalAtmWithdrawal
{
    using System;
    using System.Linq;
    using System.Transactions;
    using AtmDatabase;

    public static class AtmManager
    {
        public static Account GetAccount(AtmDbContext context, string cardNumber, string cardPin)
        {

            var account = context.Accounts.FirstOrDefault(a => a.CardNumber == cardNumber);
          
            if (account == null)
            {
                throw new InvalidOperationException("CardNumber is not exist");
            }

            account = context.Accounts.FirstOrDefault(a => a.CardPIN == cardPin && a.CardNumber == cardNumber);

            if (account == null)
            {
                throw new InvalidOperationException("Invalid PIN");
            }

            return account;
        }

        public static void CheckAmount(Account account, decimal amount)
        {

            if (amount < 0)
            {
                throw new InvalidOperationException("The " + "amount " + "must be positive.");
            }

            if (account.CardCash < amount)
            {
                throw new InvalidOperationException("Insufficient amount");
            }
        }
    }
}
