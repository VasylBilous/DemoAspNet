using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Services.Abstraction
{
    public interface IGenreService
    {
        void AddGenre(Genre genre);
        void DeleteGenre(Genre genre);
        ICollection<Genre> GetAllGenres();
        Genre Find(int id);
    }
}
