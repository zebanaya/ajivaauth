using System.ComponentModel.DataAnnotations;

namespace ajivaauth.api.Dtos
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify between 4 and 8 chars")]
        public string Password { get; set; }
    }
}