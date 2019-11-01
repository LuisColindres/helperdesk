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
            CreateMap<Company, CompanyForUpdateDto>();
            CreateMap<Gender, GenderForUpdateDto>();
            CreateMap<TicketCategory, TicketCategoryForUpdateDto>().ReverseMap();
            CreateMap<Ticket, TicketForUpdateDto>().ReverseMap();
            CreateMap<TicketDetail, TicketDetailForUpdateDto>().ReverseMap();
            CreateMap<TicketStatus, TicketStatusForUpdateDto>().ReverseMap();
            CreateMap<TicketType, TicketTypeForUpdateDto>().ReverseMap();
            CreateMap<TracingStatus, TracingStatusForUpdateDto>().ReverseMap();

        }
    }
}