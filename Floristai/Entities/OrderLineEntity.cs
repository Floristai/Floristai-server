using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floristai.Entities
{
    public class OrderLineEntity
    {
        [Key]
        public int OrderLineId { get; set; }

        public int StandardRefId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("FlowerId")]
        public int FlowerId { get; set; }

    }
}
