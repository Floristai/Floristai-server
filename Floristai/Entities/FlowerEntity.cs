using System.ComponentModel.DataAnnotations;

namespace Floristai.Entities
{
    public class FlowerEntity
    {
        [Key]
        public int FlowerId { get; set; }
        public string Name { get; set; }
    }
}
