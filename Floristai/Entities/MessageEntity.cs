using System.ComponentModel.DataAnnotations;

namespace Floristai.Entities
{
    public class MessageEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
    }
}
