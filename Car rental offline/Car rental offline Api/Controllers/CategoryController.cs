using Business;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController (RepositoryService<Category> service) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("getAllCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var company = await service.GetAll();
                if (company == null)
                    return NotFound("not found Category");


                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }
    }
}
