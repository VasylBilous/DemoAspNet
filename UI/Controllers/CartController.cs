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
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartController(ICartService _serv, IMapper _mapper)
        {
            cartService = _serv;
            mapper = _mapper;
        }
        public ActionResult CheckOut()
        {
            var cartModel = Session["UserCart"] as UserCartViewModel;
            var cart = mapper.Map<UserCart>(cartModel);
            cartService.AddUserCart(cart);
            return View(cartModel);
        }
    }
}