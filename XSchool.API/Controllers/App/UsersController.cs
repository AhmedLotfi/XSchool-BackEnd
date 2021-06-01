using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XSchool.API.BaseControllers;
using XSchool.Domain.Core.ErrorModels;
using XSchool.Services.App.Users;

namespace XSchool.API.Controllers.App
{
    public class UsersController : AuthenticatedBaseController
    {
        private readonly IUsersAppService _usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Get()
        {
            return await _usersAppService.GetAll();
        }

        // GET: api/<UsersController>/AcceptUser?{userId}
        [HttpGet]
        [Authorize(Roles = "HR")]
        [Route(nameof(AcceptUser))]
        public async Task<ApiResponse> AcceptUser(long userId)
        {
            return await _usersAppService.AcceptUser(userId);
        }

        // GET: api/<UsersController>/GetAllPagged
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route(nameof(GetAllPagged))]
        public async Task<ApiResponse> GetAllPagged(string keySearch, string sort = "desc", int pageNumber = 1, int pageSize = 10)
        {
            return await _usersAppService.GetAll(keySearch, sort, pageNumber, pageSize);
        }
    }
}
