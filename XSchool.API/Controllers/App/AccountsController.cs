using AuthenticationPlugin;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using XSchool.API.BaseControllers;
using XSchool.Domain.App.Users;
using XSchool.Domain.Core.ErrorModels;
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
        public async Task<ApiResponse> Login([FromBody] UserLoginDto userLoginDto)
        {
            return await _accountAppService.Login(userLoginDto);
        }

        [HttpPost("[action]")]
        public async Task<ApiResponse> Register([FromBody] RegisterNewUserDto userDto)
        {
            #region Valid user data
            if (await IsAnotherUserHasMyEmail(userDto.Email))
                return new ApiResponse((int)HttpStatusCode.InternalServerError, $"{userDto.Email} is already used!");

            userDto.Password = SecurePasswordHasherHelper.Hash(userDto.Password);

            bool isValidRole = Enum.IsDefined(typeof(RoleType), userDto.Role);

            if (!isValidRole) return new ApiResponse((int)HttpStatusCode.InternalServerError, "Role not valid");
            #endregion

            #region Save user
            var userMapped = _mapper.Map<User>(userDto);

            userMapped.CreationDate = DateTime.Now;
            userMapped.IsAccepted = false;
            userMapped.IsActive = true;

            await _xSchoolDbContext.Users.AddAsync(userMapped);

            await _xSchoolDbContext.SaveChangesAsync();
            #endregion

            userDto.Password = string.Empty;

            return new ApiResponse((int)HttpStatusCode.Created, "Registered Successfully", userDto);
        }

        private async Task<bool> IsAnotherUserHasMyEmail(string email) => await _xSchoolDbContext.Users.AnyAsync(user => user.Email.Equals(email));

        private string GetDefaultUserRole() => RoleType.Student.ToString();
    }
}
