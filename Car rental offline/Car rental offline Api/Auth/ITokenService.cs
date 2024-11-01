using Car_rental_offline_Api.DTOs;
using Data.Model;

namespace Car_rental_offline_Api.Auth
{
    public interface ITokenService
    {
        string GenerateToken(LoginDTO login);
    }
}
