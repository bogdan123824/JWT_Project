using AutoMapper;
using Back.Model;
using Notes.BusinessLayer.DTO;
using Notes.DataAccsessLayer.Entities;
using Notes.BusinessLayer;

namespace Presentation.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<Hashtag, HashtagDTO>().ReverseMap();
            CreateMap<AddNoteRequest, NoteDTO>();
            CreateMap<UpdateNoteRequest, NoteDTO>();
            CreateMap<NoteDTO, NoteResponse>()
                .ForMember(x => x.Hashtag, opt => opt.MapFrom(x => x.Hashtag.Name));
        }
    }
}
