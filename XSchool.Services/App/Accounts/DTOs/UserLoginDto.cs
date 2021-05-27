using System.ComponentModel.DataAnnotations;

namespace XSchool.Services.App.Accounts.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
    }
}
