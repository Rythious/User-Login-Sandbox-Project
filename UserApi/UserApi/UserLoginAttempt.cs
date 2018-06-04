using System.ComponentModel.DataAnnotations;

namespace UserApi
{
    public class UserLoginAttempt
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
