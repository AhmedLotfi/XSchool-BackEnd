using System.Threading.Tasks;
using XSchool.Domain.Core.ErrorModels;
using XSchool.Services.App.Users.DTOs;

namespace XSchool.Services.App.Accounts
{
    public interface IAccountAppService
    {
        Task<ApiResponse> Login(UserLoginDto userLoginDto);
    }
}
