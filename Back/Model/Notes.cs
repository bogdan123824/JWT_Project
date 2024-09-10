using System.ComponentModel.DataAnnotations;

namespace Back.Model
{
    public class Notes
    {
        [Key]
        public Guid Id { get; set; } 

        public string? Title { get; set; }
        public string? Text { get; set; }

        public DateTime CreatedAt { get; set; } 
        public string? UserId { get; set; }
    }
}
