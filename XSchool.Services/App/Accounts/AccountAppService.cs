using AuthenticationPlugin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using XSchool.Domain.Core.ErrorModels;
using XSchool.EntityFrameworkCore.DbContexts;
using XSchool.Services.App.Accounts.DTOs;

namespace XSchool.Services.App.Accounts
{
    public class AccountAppService
    {
        private readonly AuthService _auth;
        private readonly XSchoolDbContext _xSchoolDbContext;
        private readonly IConfiguration _configuration;

        public AccountAppService(XSchoolDbContext xSchoolDbContext, IConfiguration configuration)
        {
            _auth = new AuthService(configuration);
            _xSchoolDbContext = xSchoolDbContext;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var userExists = await _xSchoolDbContext.Users.Where(user => user.Email.Equals(userLoginDto.Email)).SingleOrDefaultAsync();

                if (userExists == null) return new ApiResponse((int)HttpStatusCode.NotFound, "User not exist !!");

                if (!SecurePasswordHasherHelper.Verify(userLoginDto.Password, userExists.Password))
                    return new ApiResponse((int)HttpStatusCode.Unauthorized, "UnAuthenticated User !!");

                var claims = new[] {
                               new Claim(JwtRegisteredClaimNames.Email, userExists.Email),
                               new Claim(ClaimTypes.Email, userExists.Email),
                               new Claim(ClaimTypes.Role , userExists.Role.ToString())
                             };

                var token = _auth.GenerateAccessToken(claims);

                var result = new
                {
                    access_token = token.AccessToken,
                    expires_in = token.ExpiresIn,
                    token_type = token.TokenType,
                    creation_Time = token.ValidFrom,
                    expiration_Time = token.ValidTo,
                    user_id = userExists.Id
                };

                return new ApiResponse((int)HttpStatusCode.OK, string.Empty, result);

            }
            catch (Exception x)
            {
                return new ApiResponse((int)HttpStatusCode.BadRequest, x.InnerException?.Message ?? x.Message);
            }
        }
    }
}
