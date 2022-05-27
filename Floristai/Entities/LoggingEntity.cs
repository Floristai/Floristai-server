using System.ComponentModel.DataAnnotations;

namespace Floristai.Entities
{
    public class LoggingEntity
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Permissions { get; set; }
        public string Time { get; set; }
        public string Method { get; set; }
    }
}
