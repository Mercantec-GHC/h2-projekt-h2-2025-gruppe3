using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class User : Common
    {
        public int Phone { get; set; }
        public required string Email { get; set; }
        public string HashedPassword { get; set; } = string.Empty;
        public string? Salt { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordBackdoor { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public virtual Role? Role { get; set; }
        public string UserInfoId { get; set; } = string.Empty;
        public UserInfo? Info { get; set; } // 1:1 navigation
        public List<Booking> Bookings { get; set; } = new();

    }

    // DTO til registrering
    public class RegisterDto
    {
        [EmailAddress(ErrorMessage = "Ugyldig email adresse")]
        [Required(ErrorMessage = "Email er påkrævet")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [MinLength(8, ErrorMessage = "Adgangskoden skal være mindst 8 tegn lang")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Adgangskoden skal indeholde mindst ét tal, ét stort bogstav, ét lille bogstav og et specialtegn")]
        public string Password { get; set; } = string.Empty;
    }

    // DTO til login
    public class LoginDto
    {
        [EmailAddress(ErrorMessage = "Ugyldig email adresse")]
        [Required(ErrorMessage = "Email er påkrævet")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [MinLength(8, ErrorMessage = "Adgangskoden skal være mindst 8 tegn lang")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Adgangskoden skal indeholde mindst ét tal, ét stort bogstav, ét lille bogstav og et specialtegn")]
        public string Password { get; set; } = string.Empty;
    }

    // DTO for getting user info - Hiding Password and userinfo
    public class UserGetDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}