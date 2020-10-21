using AutoMapper;
using BLL.Filters;
using BLL.Services.Abstraction;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        private readonly string[] sorts = new string[] { "Price ASC", "Price DESC", "Name ASC", "Name DESC" };

        public GameController(IGameService _serv, IMapper _mapper)
        {
            gameService = _serv;
            mapper = _mapper;
        }
        public ActionResult Index()
        {
            var cart = Session["UserCart"] as UserCartViewModel;

            if (cart == null)
            {
                cart = new UserCartViewModel();
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim != null)
                    {
                        cart.UserId = userIdClaim.Value;
                    }
                }

                Session["CartCount"] = 0;
                Session["UserCart"] = cart;
            }

            ViewBag.Genres = gameService.GetAllGenres();
            ViewBag.Developers = gameService.GetAllDevs();
            ViewBag.Sorts = sorts;
            var games = gameService.GetAllGames();
            var models = mapper.Map<ICollection<GameViewModel>>(games);
            return View(models);
        }
        public ActionResult Filter(string type, string name)
        {
            if (type != null && name != null)
                AddFilter(type, name);
            var games = gameService.GetAllGames(Session["GamesFilter"] as List<GamesFilter>);
            var models = mapper.Map<ICollection<GameViewModel>>(games);

            return PartialView("GamesPartial", models);
        }
        private void AddFilter(string type, string name)
        {
            var filters = Session["GamesFilter"] as List<GamesFilter>;

            if (filters == null)
                filters = new List<GamesFilter>();

            var f = filters.FirstOrDefault(x => x.Type == type && x.Name == name);
            if (f != null)
            {
                filters.Remove(f);
                Session["GamesFilter"] = filters;
                return;
            }

            var filter = new GamesFilter
            {
                Name = name,
                Type = type
            };
            if (type == "Genre")
            {
                filter.Predicate = x => x.Genre.Name == name;
            }
            else if (type == "Developer")
            {
                filter.Predicate = x => x.Developer.Name == name;
            }
            filters.Add(filter);

            Session["GamesFilter"] = filters;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Genres = gameService.GetAllGenres();
            ViewBag.Devs = gameService.GetAllDevs();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GameViewModel g, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string fullName = Server.MapPath("~/Images/") + fileName;

                if (Image != null)
                {
                    var convertedPicture = CustomImageConvertor.ConvertToBitmap(new System.Drawing.Bitmap(Image.InputStream), 200, 300);
                    if (convertedPicture != null)
                        convertedPicture.Save(fullName, ImageFormat.Jpeg);
                }
                else
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(g.Image), fullName);
                    }
                }

                g.Image = "/Images/" + fileName;
                var game = mapper.Map<Game>(g);
                gameService.AddGame(game);
                return RedirectToAction("Index");
            }
            return Create();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Genres = gameService.GetAllGenres();
            ViewBag.Devs = gameService.GetAllDevs();
            var game = gameService.Find(id);
            var model = mapper.Map<GameViewModel>(game);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, GameViewModel g, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string fullName = Server.MapPath("~/Images/") + fileName;

                if (Image != null)
                {
                    var convertedPicture = CustomImageConvertor.ConvertToBitmap(new System.Drawing.Bitmap(Image.InputStream), 200, 300);
                    if (convertedPicture != null)
                        convertedPicture.Save(fullName, ImageFormat.Jpeg);
                }
                else
                {
                    using (WebClient client = new WebClient())
                    {
                        bool a = CustomImageConvertor.IsBase64String(g.Image);
                        client.DownloadFile(new Uri(g.Image), fullName);
                    }
                }

                g.Image = "/Images/" + fileName;
                var game = mapper.Map<Game>(g);
                gameService.EditGame(game);
                return RedirectToAction("Index");
            }
            return Edit(id);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var game = gameService.Find(id);
            gameService.Delete(game);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var game = gameService.Find(id);
            var model = mapper.Map<GameViewModel>(game);
            return View(model);
        }
        [HttpPost]
        public ActionResult Buy(int id)
        {
            var cart = Session["UserCart"] as UserCartViewModel;
            if (cart == null)
                cart = new UserCartViewModel();

            var game = gameService.ReserveOrReturnNull(id);
            if (game != null)
            {
                var index = cart.GamesInCart.FindIndex(x => x.Game.Id == game.Id);
                if (index == -1)
                {
                    var modelGame = mapper.Map<GameViewModel>(game);
                    var modelGameInCart = new GamesInCartViewModel { Game = modelGame, Quantity = 1 };
                    cart.GamesInCart.Add(modelGameInCart);
                }
                else
                {
                    cart.GamesInCart[index].Quantity++;
                }
            }

            Session["UserCart"] = cart;
            Session["CartCount"] = cart.GamesInCart.Sum(x => x.Quantity);
            return RedirectToAction("Index");
        }
        public ActionResult Search(string searchInput)
        {
            var games = gameService.GetAllGames(searchInput);
            var models = mapper.Map<ICollection<GameViewModel>>(games);
            return PartialView("GamesPartial", models);
        }
        public ActionResult Sort(string sort)
        {
            var filters = Session["GamesFilter"] as List<GamesFilter>;
            var games = gameService.GetAllGames(filters, sort);
            var models = mapper.Map<ICollection<GameViewModel>>(games);
            return PartialView("GamesPartial", models);
        }
    }
}

