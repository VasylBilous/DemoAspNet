using BLL.Filters;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface IGameService
    {
        ICollection<Game> GetAllGames();
        ICollection<Game> GetAllGames(List<GamesFilter> filters);
        ICollection<Game> GetAllGames(List<GamesFilter> filters, string sort);
        ICollection<Game> GetAllGames(string query);
        void AddGame(Game model);
        void EditGame(Game model);
        IEnumerable<string> GetAllGenres();
        IEnumerable<string> GetAllDevs(); 
        Game Find(int id);
        void Delete(Game game);
        Game ReserveOrReturnNull(int id);
        void ReturnFromCart(int id);
    }
}
