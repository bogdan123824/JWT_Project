﻿namespace Presentation.Models
{
    public class PutNoteRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public string? Hashtag { get; set; }
    }
}
