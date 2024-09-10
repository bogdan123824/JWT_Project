using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.DataAccsessLayer.Entities;

namespace Back.Model
{
    public class DbContextNotes : DbContext
    {
        public DbContextNotes(DbContextOptions<DbContextNotes> options) : base(options)
        {

        }
        public virtual DbSet<Note> Notes { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
