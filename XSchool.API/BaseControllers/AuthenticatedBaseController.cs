using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XSchool.API.BaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticatedBaseController : ControllerBase
    {
    }
}
