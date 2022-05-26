using System.ComponentModel.DataAnnotations;

namespace Floristai.Entities
{
    public class OrderLineEntity
    {
        [Key]
        public int OrderLineId { get; set; }

        public int StandardRefId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public FlowerEntity Flower { get; set; }

    }
}
