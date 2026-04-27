using Presentation.WebApp.Models;

namespace Presentation.WebApp.ViewModels;

public class SignInVM
{
    public string? ReturnUrl { get; set; }
    public List<string> ExternalProviders { get; set; } = [];

    public SignInForm Form { get; set; } = new();
}
