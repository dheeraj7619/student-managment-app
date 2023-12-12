using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentDetailsInDigitalPlatform.ViewModels
{
    public class LoginViewModel
    { 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Remember Me")]
        public bool  RememberMe { get; set; }

    }
}
