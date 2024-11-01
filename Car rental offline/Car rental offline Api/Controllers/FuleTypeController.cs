using Business;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuleTypeController(RepositoryService<Fule> service) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("getAllFuleTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFuleTypes()
        {
            try
            {
                var company = await service.GetAll();
                if (company == null)
                    return NotFound("not found Fule Types");


                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }
    }
}
