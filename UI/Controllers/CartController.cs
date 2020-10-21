using AutoMapper;
using BLL.Services.Abstraction;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IGameService gameService;
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartController(ICartService _serv, IGameService gm, IMapper _mapper)
        {
            cartService = _serv;
            mapper = _mapper;
            gameService = gm;
        }
        public ActionResult CheckOut()
        {
            var cartModel = Session["UserCart"] as UserCartViewModel;
            var cart = mapper.Map<UserCart>(cartModel);
            cartService.AddUserCart(cart);
            return View(cartModel);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cartModel = Session["UserCart"] as UserCartViewModel;
            var index = cartModel.GamesInCart.FindIndex(x => x.Id == id);

            if (index != -1)
            {
                cartModel.GamesInCart.RemoveAt(index);
                Session["UserCart"] = cartModel;
                Session["CartCount"] = cartModel.GamesInCart.Sum(x => x.Quantity);
                gameService.ReturnFromCart(id);
                var cart = mapper.Map<UserCart>(cartModel);
                cartService.EditUserCart(cart);
            }

            return RedirectToAction("CheckOut");
        }
        [HttpGet]
        public ActionResult MakeOrder()
        {
            var cartModel = Session["UserCart"] as UserCartViewModel;
            var orderDetails = new OrderDetailsViewModel();
            orderDetails.UserCart = cartModel;
            Session["OrderDetails"] = orderDetails;
            return View(orderDetails);
        }
        [HttpGet]
        public ActionResult ConfirmedOrder()
        {
            var model = Session["OrderDetails"] as OrderDetailsViewModel;
            if (model != null)
            { var order = mapper.Map<OrderDetails>(model);
                cartService.PlaceOrder(order);
                return View();
            }
            return RedirectToAction("MakeOrder");
        }
    }
}