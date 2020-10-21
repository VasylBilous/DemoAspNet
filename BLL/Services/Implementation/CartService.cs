using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Repository.Abstraction;
using System;

namespace BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<UserCart> repo;
        private readonly IGenericRepository<OrderDetails> rep;

        public CartService(IGenericRepository<UserCart> _repo, IGenericRepository<OrderDetails> _rep)
        {
            repo = _repo;
            rep = _rep;
        }

        public void AddUserCart(UserCart cart)
        {
            repo.Create(cart);         
        }

        public void EditUserCart(UserCart cart)
        {
            var find = repo.Find(cart.Id);
            if (find != null)
            {
                find.GamesInCart = cart.GamesInCart;
                find.UserId = cart.UserId;
                repo.Update(find);
            }
        }

        public void PlaceOrder(OrderDetails order)
        {
            rep.Create(order);
        }
    }
}
