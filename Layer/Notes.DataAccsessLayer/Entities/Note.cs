using Notes.DataAccsessLayer.Entities;


namespace Back.Model
{
    public class Note
    {
        public Guid Id { get; set; } 

        public string? Title { get; set; }
        public string? Text { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid? HashtagId { get; set; }

        public Hashtag? Hashtag { get; set; }
    }
}
