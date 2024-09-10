namespace Presentation.Models
{
    public class NoteResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string? Hashtag { get; set; }
    }
}
