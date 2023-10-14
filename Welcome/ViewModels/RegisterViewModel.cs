using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Welcome.ViewModels
{
    public class RegisterViewModel
    {
        public string? FName { get; set; }
        public string? LName { get; set; }
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage ="Invalide Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required!")]
        [Compare("Pass", ErrorMessage = "ConfirmPassword Does Not Match Password!")]
        [DataType(DataType.Password)]
        
        public string ConfirmPass { get; set; }

        public bool IsAgree { get; set; }   


    }
}
