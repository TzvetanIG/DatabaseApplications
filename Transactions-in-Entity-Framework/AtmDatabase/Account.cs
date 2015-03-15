namespace AtmDatabase
{
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        public int Id { get; set; }

        [MaxLength(10), Required]
        public string CardNumber { get; set; }

        [MaxLength(4), Required]
        public string CardPIN { get; set; }

        [Required]
        public decimal CardCash { get; set; }
    }
}
