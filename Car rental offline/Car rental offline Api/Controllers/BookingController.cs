using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Car_rental_offline_Api.Controllers.VehicleController;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(DifferentOperationsService service) : ControllerBase
    {
      
        [HttpGet]
        [Authorize]
        [Route("GetInfoBookingByPlateNumber/plateNumber={plateNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInfoBookingByPlateNumber(string plateNumber)
        {
            if (plateNumber.Length < 1 )
                return BadRequest("Enter correct Plate Number");
            try
            {
                return Ok(await service.GetInfoBookingByPlateNumber(plateNumber));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }
    }
}
