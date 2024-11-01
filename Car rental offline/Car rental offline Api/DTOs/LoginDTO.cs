using System.ComponentModel.DataAnnotations;

namespace Car_rental_offline_Api.DTOs
{
    public class LoginDTO
    {
        public required string Username { get; set; }
        public required string  Password { get; set; }
    }
}
