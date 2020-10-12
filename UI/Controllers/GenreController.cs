using AutoMapper;
using BLL.Services.Abstraction;
using DAL.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GenreController(IGenreService _serv, IMapper _mapper)
        {
            genreService = _serv;
            mapper = _mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var genres = genreService.GetAllGenres();
            var model = mapper.Map<List<GenreViewModel>>(genres);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string newGenre)
        {
            var model = new GenreViewModel { Name = newGenre };
            var genre = mapper.Map<Genre>(model);
            genreService.AddGenre(genre);
            return Redirect("Game/Create");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var genre = genreService.Find(id);
            genreService.DeleteGenre(genre);
            return RedirectToAction("Index");
        }
    }
}