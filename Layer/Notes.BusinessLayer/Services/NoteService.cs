using AutoMapper;
using Back.Model;
using Notes.BusinessLayer.DTO;
using Notes.BusinessLayer.Interfaces;
using Notes.DataAccsessLayer.Interfaces;


namespace Notes.BusinessLayer.Services
{
    public class NoteService : INoteService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NoteService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NoteDTO>> GetAllNotes()
        {
            var notes = await _unitOfWork
                .Notess
                .GetAll();

            var notesDto = _mapper.Map<List<NoteDTO>>(notes);

            return notesDto;
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesByHashtag(HashtagDTO hashtagDTO)
        {
            var notesWithHashtag = await _unitOfWork
                .Notess
                .Find(x => x.Hashtag.Name == hashtagDTO.Name);

            var notesDtoWithHashtag = _mapper.Map<List<NoteDTO>>(notesWithHashtag);

            return notesDtoWithHashtag;
        }

        public async Task<NoteDTO> GetNoteById(Guid id)
        {
            var note = await _unitOfWork
                .Notess
                .Get(id);

            var noteDto = _mapper.Map<NoteDTO>(note);

            return noteDto;
        }

        public async Task<NoteDTO> CreateNote(NoteDTO newNote)
        {
            const int TITLE_LIMIT = 256;

            if (newNote.Title.Length > TITLE_LIMIT)
            {
                throw new Exception();
            }

            var note = _mapper.Map<Note>(newNote);

            await _unitOfWork.Notess.Create(note);
            await _unitOfWork.SaveChanges();

            return newNote;
        }

        public async Task<NoteDTO> UpdateNote(NoteDTO updatedNote)
        {
            var noteExists = await _unitOfWork
                .Notess
                .Get(updatedNote.Id) != null;

            if (!noteExists)
            {
                throw new Exception(updatedNote.Id.ToString());
            }

            var note = _mapper.Map<Note>(updatedNote);

            await _unitOfWork
                .Notess
                .Update(note);

            await _unitOfWork.SaveChanges();

            return updatedNote;
        }

        public async Task DeleteNote(Guid id)
        {
            var noteExists = await _unitOfWork
                .Notess
                .Get(id) != null;

            if (!noteExists)
            {
                throw new Exception(id.ToString());
            }

            await _unitOfWork
                .Notess
                .Delete(id);

            await _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
