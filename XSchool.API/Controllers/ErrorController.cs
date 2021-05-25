using Microsoft.AspNetCore.Mvc;
using XSchool.API.BaseControllers;
using XSchool.Domain.Core.ErrorModels;

namespace XSchool.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : AnonymouseBaseControllercs
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
