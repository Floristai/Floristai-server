namespace Floristai.Models
{
    public class Flower
    {
        public int FlowerId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
        public string Packaging { get; set; }
        public string Occasion { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public byte[] Version { get; set; }
    }
}
