using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class GamesInCartViewModel
    {
        public int Id { get; set; }
        public GameViewModel Game { get; set; }
        public UserCartViewModel UserCart { get; set; }
        [Range(0, 100000)]
        public int Quantity { get; set; }
    }
}