using Notes.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BusinessLayer.BusinessModel
{
    public class EmptyNote
    {
        public NoteDTO CreateEmpty()
        {
            return new NoteDTO()
            {
                Title = string.Empty,
                Text = string.Empty,
                CreatedAt = DateTime.MinValue,
                HashtagId = null
            };
        }
    }
}
