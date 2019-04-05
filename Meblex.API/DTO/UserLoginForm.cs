using System.ComponentModel.DataAnnotations;

namespace Meblex.API.DTO
{
    public class UserLoginForm
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Login { get; set; }
        
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password not valid, minimum eight characters, at least one letter, one number and one special character.")]
        public string Password { get; set; }
    }
}