namespace Floristai.Dto
{
    public class OrderInsertDto
    {
        public string DeliveryAddress { get; set; }
        public ICollection<OrderLineInsertDto> OrderLines { get; set; }
    }
}
