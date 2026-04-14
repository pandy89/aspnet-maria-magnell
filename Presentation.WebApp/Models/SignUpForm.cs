using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class SignUpForm
{
    [Display(Name = "Email Address", Prompt = "Enter Email Address")]
    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;
}
