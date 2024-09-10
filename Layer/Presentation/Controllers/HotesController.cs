using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLayer.DTO;
using Notes.BusinessLayer.Interfaces;
using Presentation.Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        public NotesController(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _noteService.GetAllNotes();
            var notesResponses = _mapper.Map<List<NoteResponse>>(notes);
            return Ok(notesResponses);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            var note = await _noteService.GetNoteById(id);
            var noteResponse = _mapper.Map<NoteResponse>(note);
            return Ok(noteResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] PutNoteRequest request)
        {
            var noteDto = _mapper.Map<NoteDTO>(request);
            try
            {
                await _noteService.CreateNote(noteDto);
                return Ok(noteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] NoteRequest request)
        {
            var noteDto = _mapper.Map<NoteDTO>(request);
            noteDto.Id = id;
            try
            {
                await _noteService.UpdateNote(noteDto);
                return Ok(noteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            try
            {
                await _noteService.DeleteNote(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
