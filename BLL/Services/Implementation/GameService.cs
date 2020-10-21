using Binbin.Linq;
using BLL.Filters;
using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IGenericRepository<Game> repo;
        private readonly IGenericRepository<Developer> repoDev;
        private readonly IGenericRepository<Genre> repoGenre;
        public GameService(IGenericRepository<Game> _repo, IGenericRepository<Developer> _repoDev, IGenericRepository<Genre> _repoGenre)
        {
            repo = _repo;
            repoDev = _repoDev;
            repoGenre = _repoGenre;
        }
        public void AddGame(Game model)
        {
            var dev = repoDev.GetAll().FirstOrDefault(x => x.Name == model.Developer.Name);
            if (dev != null)
                model.Developer = dev;

            var gen = repoGenre.GetAll().FirstOrDefault(x => x.Name == model.Genre.Name);
            if (gen != null)
                model.Genre = gen;

            repo.Create(model);
        }
        public void EditGame(Game model)
        {
            var find = repo.Find(model.Id);

            if (find != null)
            {
                find.Name = model.Name;
                find.Available = model.Available;
                find.Description = model.Description;
                find.Developer = model.Developer;
                find.DeveloperId = model.DeveloperId;
                find.Genre = model.Genre;
                find.GenreId = model.GenreId;
                find.Image = model.Image;
                find.Price = model.Price;
                find.Year = model.Year;
                repo.Update(find);
            }

        }
        public void Delete(Game game)
        {
            repo.Delete(game);
        }

        public Game Find(int id)
        {
            return repo.Find(id);
        }

        public IEnumerable<string> GetAllDevs()
        {
            return repoDev.GetAll().Select(x => x.Name);
        }
        public IEnumerable<string> GetAllGenres()
        {
            return repoGenre.GetAll().Select(x => x.Name);
        }

        public ICollection<Game> GetAllGames()
        {
            return repo.GetAll().ToList();
        }
        public ICollection<Game> GetAllGames(List<GamesFilter> filters)
        {
            if (filters != null && filters.Count() != 0)
            {
                var collection = new List<Expression<Func<Game, bool>>>();

                foreach (var g in filters.GroupBy(x => x.Type))
                {
                    var predicate = PredicateBuilder.Create(g.ToArray()[0].Predicate);

                    for (int i = 1; i < g.Count(); i++)
                    {
                        predicate = predicate.Or(g.ToArray()[i].Predicate);
                    }
                    collection.Add(predicate);
                }

                var res = PredicateBuilder.Create(collection[0]);
                for (int i = 1; i < collection.Count; i++)
                {
                    res = res.And(collection[i]);
                }
                return GetAllGames().Where(res.Compile()).ToList();
            }
            return GetAllGames();
        }

        public ICollection<Game> GetAllGames(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                double.TryParse(query, out double numb);
                var games = repo.GetAll().Where(x =>
                x.Description.Contains(query) ||
                string.Equals(x.Developer.Name, query, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(x.Name, query, StringComparison.OrdinalIgnoreCase) ||
                x.Price == numb ||
                x.Year == numb).ToList();
                return games;
            }
            return repo.GetAll().ToList();
        }

        public ICollection<Game> GetAllGames(List<GamesFilter> filters, string sort)
        {
            var notSorted = GetAllGames(filters).ToList();
            var sorted = new List<Game>();
            switch (sort)
            {
                case "Price ASC":
                    sorted = notSorted.OrderBy(x => x.Price).ToList();
                    break;
                case "Price DESC":
                    sorted = notSorted.OrderByDescending(x => x.Price).ToList();
                    break;
                case "Name ASC":
                    sorted = notSorted.OrderBy(x => x.Name).ToList();
                    break;
                case "Name DESC":
                    sorted = notSorted.OrderByDescending(x => x.Name).ToList();
                    break;
            }
            return sorted;
        }

        public void AddGenre(Genre genre)
        {
            repoGenre.Create(genre);
        }

        public void DeleteGenre(Genre genre)
        {
            repoGenre.Delete(genre);
        }

        public void AddDeveloper(Developer developer)
        {
            repoDev.Create(developer);
        }

        public void DeleteDeveloper(Developer developer)
        {
            repoDev.Delete(developer);
        }

        public Game ReserveOrReturnNull(int id)
        {
            var item = repo.Find(id);

            if (item != null && item.Available > 0)
            {
                var edited = item;
                edited.Available--;
                EditGame(edited);
                return item;
            }

            return null;
        }

        public void ReturnFromCart(int id)
        {
            var item = repo.Find(id);

            if (item != null)
            {
                var edited = item;
                edited.Available++;
                EditGame(edited);
            }
        }
    }
}
