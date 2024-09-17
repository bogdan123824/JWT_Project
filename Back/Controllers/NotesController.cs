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
            DbContextNotes Notecontext,
            UserManager<IdentityUser> userManager)
        {
            _context = Notecontext;
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

            var userId = await GetUserId();

            if (note.UserId != userId)
            {
                return Forbid();
            }

            return Ok(note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Back.Model.Notes notes)
        {
            var userId = await GetUserId();

            var note = await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (note is null)
            {
                return NotFound();
            }

            if (note.UserId != userId)
            {
                return Forbid();
            }

            note.Title = notes.Title;
            note.Text = notes.Text;

            await _context.SaveChangesAsync();

            return Ok(note); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Back.Model.Notes note)
        {
            var userId = await GetUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            note.CreatedAt = DateTime.Now;
            note.UserId = userId;

            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note); 
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var userId = await GetUserId();

            var note = await _context.Notes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (note is null)
            {
                return NotFound();
            }

            if (note.UserId != userId)
            {
                return Forbid();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<string?> GetUserId()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            var user = await _userManager
                .FindByNameAsync(username!);

            return user?.Id;
        }
    }
}
