using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class RegisterPasswordForm
{
    [Display(Name = "Password", Prompt = "Enter Password")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*?\\d)(?=.*?[!@#$%^&*()_+={}\\[\\]\\\\:;\"'<>,.?/-]).{8,}$", ErrorMessage = "Password must be at least 8 characters and contain at least one digit and one special character.")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password ", Prompt = "Confirm Password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Your passwords do not match.")]
    public string ConfirmPassword { get; set; } = null!;
}
