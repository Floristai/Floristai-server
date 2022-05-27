using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floristai.Entities
{
    public class OrderEntity
    {
        [Key]

        public int OrderId { get; set; }

        [ForeignKey("UserId")]
        public int ClientId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        [ForeignKey("OrderId")]
        public ICollection<OrderLineEntity> OrderLines { get; set; }

      
    }
}
