using System;
using XSchool.Domain.App.Audit.Interfaces;
using XSchool.Services.App.Audits;
using static XSchool.Domain.Helpers.Enums.RoleTypes;

namespace XSchool.Services.App.Users.DTOs
{
    public class GetAllUsersDto : AuditEntityDto<long>, IPassive
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public RoleType Role { get; set; }

        public bool IsActive { get; set; }

        public bool IsAccepted { get; set; }
    }
}
