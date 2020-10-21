using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface ICartService
    {
        void AddUserCart(UserCart cart);
        void EditUserCart(UserCart cart);
        void PlaceOrder(OrderDetails order);
    }
}
