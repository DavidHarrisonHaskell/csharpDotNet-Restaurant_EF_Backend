using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ModelMetadataType(typeof(UserMetaData))]
public partial class User
{

}

public class UserMetaData
{
    [Required(ErrorMessage = "Missing First Name.")]
    [MinLength(2, ErrorMessage = "FirstName must not be less than two characters.")]
    [MaxLength(20, ErrorMessage = "FirstName must not be greater than 20 characters.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Missing Last Name.")]
    [MinLength(2, ErrorMessage = "LastName must not be less than two characters.")]
    [MaxLength(50, ErrorMessage = "LastName must not be greater than 50 characters.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Missing Email.")]
    [MinLength(2, ErrorMessage = "Email must not be less than two characters.")]
    [MaxLength(100, ErrorMessage = "Email must not be greater than 100 characters.")]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Missing Password.")]
    [MinLength(2, ErrorMessage = "Password must not be less than two characters.")]
    [MaxLength(1000, ErrorMessage = "Password must not be greater than 1000 characters.")]
    public string Password { get; set; } = null!;

}
