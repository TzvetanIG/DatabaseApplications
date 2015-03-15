namespace MsSqlDatabase
{
    using System.ComponentModel.DataAnnotations;

    public class PieceOfNews
    {
        public int Id { get; set; }

        [ConcurrencyCheck] 
        public string Content { get; set; }
    }
}
