using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class UserCartViewModel
    {
        public int Id { get; set; }
        public List<GamesInCartViewModel> GamesInCart { get; set; }
        public string UserId { get; set; }

        public UserCartViewModel()
        {
            GamesInCart = new List<GamesInCartViewModel>();
        }
    }
}