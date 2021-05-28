using System;
using System.ComponentModel.DataAnnotations;
using static XSchool.Domain.Helpers.Enums.RoleTypes;

namespace XSchool.Services.App.Accounts.DTOs
{
    public class RegisterNewUserDto
    {
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(150)]
        public RoleType Role { get; set; }
    }
}
