using System.ComponentModel.DataAnnotations;

namespace MidtermProject.Models;

public class RegisterModel
{
    [Required, MinLength(2)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept terms")]
    public bool AgreeToTerms { get; set; }
}

public class LoginModel
{
    [Required]
    public string EmailOrUsername { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}