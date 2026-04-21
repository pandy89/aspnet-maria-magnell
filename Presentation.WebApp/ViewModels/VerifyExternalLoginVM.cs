using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.ViewModels;

public class VerifyExternalLoginVM
{
    public string Email { get; set; } = null!;
    public string? ReturnUrl { get; set; }

    [Required(ErrorMessage = "Du måste ange verifikationkod.")]
    public string Code { get; set; } = null!;

}
