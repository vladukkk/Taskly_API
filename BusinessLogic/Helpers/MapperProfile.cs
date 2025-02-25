using AutoMapper;
using BusinessLogic.DTOs.Priority;
using BusinessLogic.DTOs.Quotes;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.DTOs.Task;
using BusinessLogic.DTOs.User;
using BusinessLogic.DTOs.User.Auth;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<UserEntity, LoginDTO>().ReverseMap();
            CreateMap<UserEntity, RegisterDTO>().ReverseMap();

            CreateMap<IdentityRole, RoleDTO>().ReverseMap();

            CreateMap<TaskEntity, TaskDTO>().ReverseMap();
            CreateMap<TaskEntity, TaskAddDTO>().ReverseMap();
            CreateMap<TaskEntity, TaskUpdateDTO>().ReverseMap();

            CreateMap<TaskUpdateDTO, TaskAddDTO>().ReverseMap();

            CreateMap<PriorityEntity, PriorityDTO>().ReverseMap();
            CreateMap<PriorityEntity, PriorityAddDTO>().ReverseMap();
            CreateMap<PriorityEntity, PriorityUpdateDTO>().ReverseMap();

            CreateMap<TagEntity, TagDTO>().ReverseMap();
            CreateMap<TagEntity, TagAddDTO>().ReverseMap();
            CreateMap<TagEntity, TagUpdateDTO>().ReverseMap();

            CreateMap<QuoteEntity, QuoteDTO>().ReverseMap();
            CreateMap<QuoteEntity, QuoteAddDTO>().ReverseMap();
        }
    }
}
