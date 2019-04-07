using System.ComponentModel.DataAnnotations;

namespace Meblex.API.DTO
{
    public class UserRegisterForm
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password not valid, minimum eight characters, at least one letter, one number and one special character.")]

        public string Password { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Street { get; set; }

        [StringLength(32)]
        public string Address { get; set; }

        [StringLength(32)]
        public string State { get; set; }
        [StringLength(32)]
        public string City { get; set; }

//        [RegularExpression(@"\b\d{5}\b/g", ErrorMessage = "PostCode not valid")]
        public int PostCode { get; set; }
    }
}
