using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using XSchool.Domain.App.Users;
using XSchool.Domain.Core.ErrorModels;
using XSchool.Domain.Core.Helpers;
using XSchool.EntityFrameworkCore.DbContexts;
using XSchool.Services.App.Users.DTOs;

namespace XSchool.Services.App.Users
{
    public class UsersAppService : IUsersAppService
    {
        private readonly XSchoolDbContext _xSchoolDbContext;
        private readonly IMapper _mapper;

        public UsersAppService(XSchoolDbContext xSchoolDbContext, IMapper mapper)
        {
            _xSchoolDbContext = xSchoolDbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetAll()
        {
            var data = await _xSchoolDbContext.Users.ToListAsync();

            var dataMapped = _mapper.Map<IReadOnlyList<User>>(data);

            return new ApiResponse((int)HttpStatusCode.OK, "Data Retreived Successfully", dataMapped);
        }

        public async Task<ApiResponse> GetAll(string keySearch, string sort = "desc", int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var data = _xSchoolDbContext.Users.Where(user => string.IsNullOrEmpty(keySearch)
                          || user.UserName.Contains(keySearch) || user.Email.Contains(keySearch)
                          || user.FirstName.Contains(keySearch) || user.LastName.Contains(keySearch));

                data = data.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                switch (sort)
                {
                    case "desc":
                        {
                            var dataCounts = await data.CountAsync();
                            var dataSorted = _mapper.Map<IReadOnlyList<GetAllUsersDto>>(data.OrderByDescending(movie => movie.UserName).ToListAsync());
                            var pagingationResult = new Pagination<GetAllUsersDto>(pageNumber, pageSize, dataCounts, dataSorted);

                            return new ApiResponse((int)HttpStatusCode.OK, "Data Retreived Successfully", pagingationResult);
                        }

                    case "asc":
                        {
                            var dataCounts = await data.CountAsync();
                            var dataSorted = _mapper.Map<IReadOnlyList<GetAllUsersDto>>(data.OrderBy(movie => movie.UserName).ToListAsync());
                            var pagingationResult = new Pagination<GetAllUsersDto>(pageNumber, pageSize, dataCounts, dataSorted);

                            return new ApiResponse((int)HttpStatusCode.OK, "Data Retreived Successfully", pagingationResult);
                        }

                    default:
                        {
                            var dataCounts = await data.CountAsync();
                            var dataMapped = _mapper.Map<IReadOnlyList<GetAllUsersDto>>(data.ToListAsync());
                            var pagingationResult = new Pagination<GetAllUsersDto>(pageNumber, pageSize, dataCounts, dataMapped);

                            return new ApiResponse((int)HttpStatusCode.OK, "Data Retreived Successfully", pagingationResult);
                        }
                }

            }
            catch (Exception x)
            {
                return new ApiResponse((int)HttpStatusCode.BadRequest, x.InnerException?.Message ?? x.Message);
            }
        }
    }
}
