using Back.Model;
using Microsoft.EntityFrameworkCore;
using Notes.DataAccsessLayer.Entities;
using Notes.DataAccsessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DataAccsessLayer.Repositories
{
    public class NoteRepos : IRepository<Note>
    {
        private readonly DbContextNotes _context;

        public NoteRepos(DbContextNotes context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            return await _context
                .Notes
                .Include(x => x.Hashtag)
                .ToListAsync();
        }

        public async Task<Note?> Get(Guid id)
        {
            return await _context
                .Notes
                .Include(x => x.Hashtag)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Note>> Find(Func<Note, bool> predicate)
        {
            return _context
                .Notes
                .Include(x => x.Hashtag)
                .Where(predicate).ToList();
        }

        public async Task Create(Note item)
        {
            if (item.Hashtag is not null &&
                item.Hashtag.Name?.Length > 0)
            {
                var existingHashtag = await _context
                    .Hashtags
                    .FirstOrDefaultAsync(x => x.Name == item.Hashtag.Name);

                if (existingHashtag is not null)
                {
                    item.HashtagId = existingHashtag.Id;
                }
                else
                {
                    await _context
                        .Hashtags
                        .AddAsync(new Hashtag { Name = item.Hashtag.Name });
                }
            }

            await _context
                .Notes
                .AddAsync(item);
        }

        public async Task Update(Note item)
        {
            if (item.Hashtag is not null &&
                item.Hashtag.Name?.Length > 0)
            {
                var existingHashtag = await _context
                    .Hashtags
                    .FirstOrDefaultAsync(x => x.Name == item.Hashtag.Name);

                if (existingHashtag is not null)
                {
                    item.HashtagId = existingHashtag.Id;
                }
                else
                {
                    await _context
                        .Hashtags
                        .AddAsync(new Hashtag { Name = item.Hashtag.Name });
                }
            }

            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task Delete(Guid id)
        {
            var note = await _context
                .Notes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (note is null)
            {
                return;
            }

            _context.Notes.Remove(note);
        }
    }
}
