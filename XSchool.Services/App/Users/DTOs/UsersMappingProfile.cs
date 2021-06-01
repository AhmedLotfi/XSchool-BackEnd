using AutoMapper;
using XSchool.Domain.App.Users;

namespace XSchool.Services.App.Users.DTOs
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            _ = CreateMap<GetAllUsersDto, User>().ReverseMap();
        }
    }
}
