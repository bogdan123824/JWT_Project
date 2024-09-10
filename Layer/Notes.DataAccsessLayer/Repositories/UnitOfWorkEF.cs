using Back.Model;
using Notes.DataAccsessLayer.Entities;
using Notes.DataAccsessLayer.Interfaces;


namespace Notes.DataAccsessLayer.Repositories
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private bool disposed = false;

        private readonly DbContextNotes _context;
        private readonly NoteRepos _noteRepos;
        private readonly HashtagRepos _hashtagRepos;

        public IRepository<Note> Notess => _noteRepos;

        public IRepository<Hashtag> Hashtags => _hashtagRepos;

        public UnitOfWorkEF(DbContextNotes context)
        {
            _context = context;
            _noteRepos = new NoteRepos(context);
            _hashtagRepos = new HashtagRepos(context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                _context.Dispose();
                disposed = true;
            }
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}
