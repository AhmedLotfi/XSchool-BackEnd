using System;
using System.ComponentModel.DataAnnotations;
using XSchool.Domain.App.Audit;
using static XSchool.Domain.Helpers.Enums.RoleTypes;

namespace XSchool.Domain.App.Users
{
    public class User : AuditedEntity<long>
    {
        #region Props

        [Required]
        [MaxLength(250)]
        public string UserName { get; protected set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; protected set; }

        [Required]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; protected set; }

        [MaxLength(200)]
        public string FirstName { get; protected set; }

        [MaxLength(200)]
        public string LastName { get; protected set; }

        public DateTime DateOfBirth { get; protected set; }

        [Required]
        [MaxLength(150)]
        public RoleType Role { get; protected set; }
        #endregion

        protected User() { }
    }
}
