using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace Welcome.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalide Email Address")]
        public string Email { get; set; }
        
    }
}
