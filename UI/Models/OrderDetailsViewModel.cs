using DAL.Entities;

namespace UI.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public UserCartViewModel UserCart { get; set; }
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
    }
}