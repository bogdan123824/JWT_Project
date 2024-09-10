using Back.Model;
using Notes.DataAccsessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DataAccsessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Note> Notess { get; }
        IRepository<Hashtag> Hashtags { get; }
        Task SaveChanges();
    }
}
