using Business;
using Car_rental_offline_Api.Auth;
using Car_rental_offline_Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController(LoginService service,ITokenService tokenService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] LoginDTO login)
        {
            var res = await service.Login(login.Username, login.Password);
            if (res)
            {
                var token = tokenService.GenerateToken(login);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
