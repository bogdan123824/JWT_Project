using AutoMapper;
using Notes.BusinessLayer.DTO;
using Notes.BusinessLayer.Interfaces;
using Notes.DataAccsessLayer.Entities;
using Notes.DataAccsessLayer.Interfaces;

namespace Notes.BusinessLogicLayer.Services
{
    public class HashtagService : IHashtagService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HashtagService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HashtagDTO>> GetAllHashtags()
        {
            var hashtags = await _unitOfWork
                .Hashtags
                .GetAll();

            var hashtagsDto = _mapper.Map<List<HashtagDTO>>(hashtags);

            return hashtagsDto;
        }

        public async Task<HashtagDTO> GetHashtagById(Guid id)
        {
            var hashtag = await _unitOfWork
                .Hashtags
                .Get(id);

            var hashtagDto = _mapper.Map<HashtagDTO>(hashtag);

            return hashtagDto;
        }

        public async Task<HashtagDTO> CreateHashtag(HashtagDTO newHashtag)
        {
            var hashtag = _mapper.Map<Hashtag>(newHashtag);

            await _unitOfWork.Hashtags.Create(hashtag);
            await _unitOfWork.SaveChanges();

            return newHashtag;
        }

        public async Task<HashtagDTO> UpdateHashtag(HashtagDTO updatedHashtag)
        {
            var hashtagExists = await _unitOfWork
                .Notess
                .Get(updatedHashtag.Id) != null;

            if (!hashtagExists)
            {
                throw new Exception(updatedHashtag.Id.ToString());
            }

            var hashtag = _mapper.Map<Hashtag>(updatedHashtag);

            await _unitOfWork
                .Hashtags
                .Update(hashtag);

            await _unitOfWork.SaveChanges();

            return updatedHashtag;
        }

        public async Task DeleteHashtag(Guid id)
        {
            var hashtagExists = await _unitOfWork
                .Notess
                .Get(id) != null;

            if (!hashtagExists)
            {
                throw new Exception(id.ToString());
            }

            await _unitOfWork
                .Hashtags
                .Delete(id);

            await _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}