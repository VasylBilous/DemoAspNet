using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class GamesInCart
    {
        [Key]
        public int Id { get; set; }
        public int? GameId { get; set; }
        public virtual Game Game { get; set; }
        public int? UserCartId { get; set; }
        public virtual UserCart UserCart { get; set; }
        public int Quantity { get; set; }      
    }
}
