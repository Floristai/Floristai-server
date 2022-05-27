namespace Floristai.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }

    }
}
