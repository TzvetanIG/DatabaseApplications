namespace AtmDatabase
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TransactionHistory
    {
        public int Id { get; set; }

        [MaxLength(10), Required]
        public string CardNumber { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
