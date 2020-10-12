using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IGenericRepository<Genre> repoGenre;

        public GenreService(IGenericRepository<Genre> _repoGenre)
        {
            repoGenre = _repoGenre;
        }

        public void AddGenre(Genre genre)
        {
            repoGenre.Create(genre);
        }

        public void DeleteGenre(Genre genre)
        {
            repoGenre.Delete(genre);
        }

        public Genre Find(int id)
        {
            return repoGenre.Find(id);
        }

        public ICollection<Genre> GetAllGenres()
        {
            return repoGenre.GetAll().ToList();
        }
    }
}
