using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class UserCart
    {
        [Key]
        public int Id { get; set; }
        public ICollection<GamesInCart> GamesInCart { get; set; }
        public string UserId { get; set; }
     
        public UserCart()
        {
            GamesInCart = new List<GamesInCart>();
        }
    }
}
