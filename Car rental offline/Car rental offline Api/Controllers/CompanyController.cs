using Business;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController (RepositoryService<Company> service) : ControllerBase
    {


        [HttpGet]
        [Authorize]
        [Route("getAllCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {
                var company = await service.GetAll();
                if (company == null)
                    return NotFound("not found company");
                
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }



        [HttpGet]
        [Route("GetVehicleLogo/FileName={FileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicleLogo(string FileName)
        {
            if (FileName.Length < 1)
                return BadRequest("Enter correct Name");

            try
            {
                var uploadDirectory = @"C:\Users\Asus\Desktop\Projects\Car rental\Car rental online\Car rental by EF\Logos";
                var filePath = Path.Combine(uploadDirectory, FileName);

                if (!System.IO.File.Exists(filePath))
                    return NotFound("Image not found.");

                var image = System.IO.File.OpenRead(filePath);
                var mimeType = GetMimeType(filePath);

                return File(image, mimeType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }

        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }
    }
}
