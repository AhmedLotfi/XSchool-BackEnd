using System.Threading.Tasks;
using XSchool.Domain.Core.ErrorModels;

namespace XSchool.Services.App.Users
{
    public interface IUsersAppService
    {
        public Task<ApiResponse> GetAll();

        public Task<ApiResponse> AcceptUser(long userId);

        public Task<ApiResponse> GetAll(string keySearch, string sort = "desc", int pageNumber = 1, int pageSize = 10);
    }
}
