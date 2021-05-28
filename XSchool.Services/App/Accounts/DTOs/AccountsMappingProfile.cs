using AutoMapper;
using XSchool.Domain.App.Users;

namespace XSchool.Services.App.Accounts.DTOs
{
    public class AccountsMappingProfile : Profile
    {
        public AccountsMappingProfile()
        {
            _ = CreateMap<RegisterNewUserDto, User>();
        }
    }
}
