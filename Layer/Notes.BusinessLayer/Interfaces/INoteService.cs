using Notes.BusinessLayer.DTO;

namespace Notes.BusinessLayer.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDTO>> GetAllNotes();
        Task<NoteDTO> GetNoteById(Guid id);
        Task<IEnumerable<NoteDTO>> GetNotesByHashtag(HashtagDTO hashtagDTO);
        Task<NoteDTO> CreateNote(NoteDTO newNote);
        Task<NoteDTO> UpdateNote(NoteDTO updatedNote);
        Task DeleteNote(Guid id);
        void Dispose();
    }
}
