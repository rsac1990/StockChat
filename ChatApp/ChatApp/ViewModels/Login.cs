using System.ComponentModel.DataAnnotations;

namespace ChatApp.ViewModels
{
    public class Login
    {
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
