using AutoMapper;
using BusinessLogic.DTOs.Priority;
using BusinessLogic.DTOs.Tag;
using DataAccess.EntityModels;

namespace BusinessLogic.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PriorityEntity, PriorityDTO>().ReverseMap();
            CreateMap<PriorityEntity, PriorityAddDTO>().ReverseMap();
            CreateMap<PriorityEntity, PriorityUpdateDTO>().ReverseMap();

            CreateMap<TagEntity, TagDTO>().ReverseMap();
            CreateMap<TagEntity, TagAddDTO>().ReverseMap();
            CreateMap<TagEntity, TagUpdateDTO>().ReverseMap();
        }
    }
}
