using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int? UserCartId { get; set; }
        public virtual UserCart UserCart { get; set; }
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
    }
}
