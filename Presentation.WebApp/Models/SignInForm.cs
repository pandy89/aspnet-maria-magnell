using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class SignInForm
{
    [Display(Name = "Email Address", Prompt = "Enter Email Address")]
    [Required(ErrorMessage = "Email address is required")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter Password")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
