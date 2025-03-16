using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ilmV3.Application.Account.Login;
public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; } = string.Empty;
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
}
