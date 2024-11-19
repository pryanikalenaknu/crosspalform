using System.ComponentModel.DataAnnotations;

namespace Lab5.Models;

public class UserLoginModel
{

    [Required(ErrorMessage = "Ви не можете пропустити це поле!")]
    [EmailAddress(ErrorMessage = "Ви ввели не електронну адресу")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Ви не можете пропустити це поле!")]
    public string Password { get; set; }
}