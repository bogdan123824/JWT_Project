using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BusinessLayer.DTO
{
    public class NoteDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public Guid? HashtagId { get; set; }

        public HashtagDTO? Hashtag { get; set; }
    }
}
