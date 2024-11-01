using Business;
using Car_rental_offline_Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/Return")]
    [ApiController]
    public class ReturnController(DifferentOperationsService service) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("AddReturn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddReturnByPlateNumber(AddReturnDTO dTO)
        {
            if (dTO.plateNumber.Length < 1)
                return BadRequest("Enter correct Plate Number");
            try
            {
                return Ok(await service.AddReturnByPlateNumber(dTO.plateNumber, dTO.ConsumedMilaeage, dTO.FinalCheckNotes));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }
    }
}
