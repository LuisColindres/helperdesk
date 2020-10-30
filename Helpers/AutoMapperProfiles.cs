using AutoMapper;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Role,RoleForAddDto>();
            CreateMap<User,UserForListDto>();
            CreateMap<UserForListDto, UserForUpdateDto>();
            CreateMap<Company, CompanyForUpdateDto>();
            CreateMap<Gender, GenderForUpdateDto>();
            CreateMap<TicketCategory, TicketCategoryForUpdateDto>().ReverseMap();
            CreateMap<TicketForListDto, TicketForUpdateDto>().ReverseMap();
            CreateMap<TicketForListDto, TicketForAssingDto>().ReverseMap();
            CreateMap<TicketDetail, TicketDetailForUpdateDto>().ReverseMap();
            CreateMap<TicketStatus, TicketStatusForUpdateDto>().ReverseMap();
            CreateMap<TicketType, TicketTypeForUpdateDto>().ReverseMap();
            CreateMap<TracingStatus, TracingStatusForUpdateDto>().ReverseMap();
            CreateMap<Management, ManagamentForUpdateDto>().ReverseMap();
            CreateMap<Sessions, SessionForUpdateDto>().ReverseMap();
            CreateMap<File, FileForUpdateDto>().ReverseMap();
        }
    }
}