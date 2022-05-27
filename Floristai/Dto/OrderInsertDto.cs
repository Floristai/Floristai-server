namespace Floristai.Dto
{
    public class OrderInsertDto
    {
        public ICollection<OrderLineInsertDto> OrderLines { get; set; }
    }
}
