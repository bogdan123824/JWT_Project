using Notes.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BusinessLayer.Interfaces
{
    public interface IHashtagService
    {
        Task<IEnumerable<HashtagDTO>> GetAllHashtags();
        Task<HashtagDTO> GetHashtagById(Guid id);
        Task<HashtagDTO> CreateHashtag(HashtagDTO newHashtag);
        Task<HashtagDTO> UpdateHashtag(HashtagDTO updatedHashtag);
        Task DeleteHashtag(Guid id);
        void Dispose();
    }
}
