using System.ComponentModel.DataAnnotations;

namespace UserApi
{
    public class UserPost
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
