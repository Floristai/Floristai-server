using System.ComponentModel.DataAnnotations;

namespace Floristai.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }
}
