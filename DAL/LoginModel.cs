using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AuthContract;

public class LoginModel
{
    [Required]
    [MinLength(4)]
    public required string Username { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}
