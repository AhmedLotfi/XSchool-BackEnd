using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XSchool.API.BaseControllers;
using XSchool.Domain.Core.ErrorModels;
using XSchool.Services.App.Users;

namespace XSchool.API.Controllers.App
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AuthenticatedBaseController
    {
        private readonly IUsersAppService _usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ApiResponse> Get()
        {
            return await _usersAppService.GetAll();
        }

        // GET: api/<UsersController>/GetAllPagged
        [HttpGet]
        [Route(nameof(GetAllPagged))]
        public async Task<ApiResponse> GetAllPagged(string keySearch, string sort = "desc", int pageNumber = 1, int pageSize = 10)
        {
            return await _usersAppService.GetAll(keySearch, sort, pageNumber, pageSize);
        }
    }
}
