using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using Back.Model;
using Microsoft.AspNetCore.Identity;

namespace Notes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly DbContextNotes _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NotesController(
            DbContextNotes context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (note is null)
            {
                return NotFound();
            }

            var currentUserId = await GetUserId();

            if (note.UserId != currentUserId)
            {
                return Forbid();
            }

            return Ok(note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Back.Model.Notes note)
        {
            var currentUserId = await GetUserId();

            var existingNote = await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingNote is null)
            {
                return NotFound();
            }

            if (existingNote.UserId != currentUserId)
            {
                return Forbid();
            }

            existingNote.Title = note.Title;
            existingNote.Text = note.Text;

            await _context.SaveChangesAsync();

            return Ok(existingNote); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Back.Model.Notes note)
        {
            var currentUserId = await GetUserId();

            if (currentUserId is null)
            {
                return Unauthorized();
            }

            note.CreatedAt = DateTime.Now;
            note.UserId = currentUserId;

            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note); 
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var currentUserId = await GetUserId();

            var existingNote = await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingNote is null)
            {
                return NotFound();
            }

            if (existingNote.UserId != currentUserId)
            {
                return Forbid();
            }

            _context.Notes.Remove(existingNote);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<string?> GetUserId()
        {
            var currentUsername = User.Identity?.Name;

            if (string.IsNullOrEmpty(currentUsername))
            {
                return null;
            }

            var currentUser = await _userManager
                .FindByNameAsync(currentUsername!);

            return currentUser?.Id;
        }
    }
}
