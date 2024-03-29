using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
