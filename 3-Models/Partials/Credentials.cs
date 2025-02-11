using System.ComponentModel.DataAnnotations;

namespace Talent;

public class Credentials
{
    // what needs to be entered to gain entry
    [Required(ErrorMessage = "Missing Email.")]
    [MinLength(2, ErrorMessage = "Email must not be less than two characters.")]
    [MaxLength(100, ErrorMessage = "Email must not be greater than 100 characters.")]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Missing Password. ")]
    [MinLength(2, ErrorMessage = "Password must not be less than two characters.")]
    [MaxLength(100, ErrorMessage = "Password must not be greater than 100 characters.")]
    public string Password { get; set; } = null!;
}