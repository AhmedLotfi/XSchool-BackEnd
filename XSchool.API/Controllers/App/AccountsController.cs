using AuthenticationPlugin;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using XSchool.API.BaseControllers;
using XSchool.Domain.App.Users;
using XSchool.EntityFrameworkCore.DbContexts;
using XSchool.Services.App.Accounts;
using XSchool.Services.App.Accounts.DTOs;
using static XSchool.Domain.Helpers.Enums.RoleTypes;

namespace XSchool.API.Controllers
{
    public class AccountsController : AnonymouseBaseControllercs
    {
        private readonly IAccountAppService _accountAppService;
        private readonly XSchoolDbContext _xSchoolDbContext;
        private readonly IMapper _mapper;

        public AccountsController(IAccountAppService accountAppService, XSchoolDbContext xSchoolDbContext, IMapper mapper)
        {
            _accountAppService = accountAppService;
            _xSchoolDbContext = xSchoolDbContext;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var loginResult = await _accountAppService.Login(userLoginDto);

            if (loginResult.Success) return Ok(loginResult);

            return BadRequest(loginResult);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterNewUserDto userDto)
        {
            try
            {
                if (await IsAnotherUserHasMyEmail(userDto.Email)) return BadRequest($"{userDto.Email} is already used!");

                userDto.Password = SecurePasswordHasherHelper.Hash(userDto.Password);

                var userMapped = _mapper.Map<User>(userDto);

                await _xSchoolDbContext.Users.AddAsync(userMapped);

                await _xSchoolDbContext.SaveChangesAsync();

                return Created(HttpContext.Request.Path, userDto);
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException?.Message ?? x.Message);
            }
        }

        private async Task<bool> IsAnotherUserHasMyEmail(string email)
        {
            return await _xSchoolDbContext.Users.AnyAsync(user => user.Email.Equals(email));
        }

        private string GetDefaultUserRole() => RoleType.Student.ToString();
    }
}
