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
    public class HashtagRepos : IRepository<Hashtag>
    {
        private readonly DbContextNotes _context;

        public HashtagRepos(DbContextNotes context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hashtag>> GetAll()
        {
            return await _context.Hashtags.ToListAsync();
        }

        public async Task<Hashtag?> Get(Guid id)
        {
            return await _context.Hashtags.FindAsync(id);
        }

        public async Task<IEnumerable<Hashtag>> Find(Func<Hashtag, bool> predicate)
        {
            return _context.Hashtags.Where(predicate).ToList();
        }

        public async Task Create(Hashtag item)
        {
            await _context.Hashtags.AddAsync(item);
        }

        public async Task Update(Hashtag item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task Delete(Guid id)
        {
            var hashtag = await _context.Hashtags.FindAsync(id);
            if (hashtag is null)
            {
                return;
            }
            _context.Hashtags.Remove(hashtag);
        }
    }
}
