using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Back.Model
{
    public class DbContextNotes : IdentityDbContext<IdentityUser>
    {
        public DbContextNotes(DbContextOptions<DbContextNotes> options) : base(options)
        {

        }
        public virtual DbSet<Notes> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
