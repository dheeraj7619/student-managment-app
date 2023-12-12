using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentDetailsInDigitalPlatform.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not matched")]

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
