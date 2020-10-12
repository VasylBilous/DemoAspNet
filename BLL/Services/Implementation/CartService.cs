using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Repository.Abstraction;
using System;

namespace BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<UserCart> repo;

        public CartService(IGenericRepository<UserCart> _repo)
        {
            repo = _repo;
        }

        public void AddUserCart(UserCart cart)
        {
            repo.Create(cart);         
        }
    }
}
