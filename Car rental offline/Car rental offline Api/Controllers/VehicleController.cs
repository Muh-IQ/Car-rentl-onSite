using Business;
using Car_rental_offline_Api.DTOs;
using Car_rental_offline_Api.HandleImages;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace Car_rental_offline_Api.Controllers
{
    [Route("api/Vehicle")]
    [ApiController]
    public class VehicleController(RepositoryService<Vehicle> service) : ControllerBase
    {
        public enum Modes { All = 1, Available, UnAvailable }

        [HttpGet]
        [Authorize]
        [Route("GetAllVehicle/pageIndex={pageIndex}/pageSize={pageSize}/modes={modes}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVehicle(int pageIndex, int pageSize , Modes modes)
        {
            if (pageIndex < 1 || pageSize < 1)
                return BadRequest("Enter correct data");

            try
            {
                //you can pass "null " replacement 'expression'  
                Expression<Func<Vehicle, bool>> expression = (modes == Modes.All ? null :
                        (modes == Modes.Available ? x => x.IsAvailableForRent == true : x => x.IsAvailableForRent == false));

                var res = await service.GetAllByTransactionAsync(pageIndex, pageSize, expression
                    , ["company", "fule", "category"]);
                if (res == null)
                    return NotFound("Not found vehicle");

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }


        [HttpGet]
        [Authorize]
        [Route("GetAllVehicle/pageIndex={pageIndex}/pageSize={pageSize}/modes={modes}/plateNumber={plateNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVehicleByPlateNumber(int pageIndex = 1, int pageSize = 4, Modes modes = Modes.UnAvailable , string plateNumber = null)
        {
            if (pageIndex < 1 || pageSize < 1)
                return BadRequest("Enter correct data");

            try
            {
                Expression<Func<Vehicle, bool>> expression = (
                        modes == Modes.All ?
                            (v => v.PlateNumber.StartsWith(plateNumber)) :
                            (modes == Modes.Available ?
                                (x => x.IsAvailableForRent == true && x.PlateNumber.StartsWith(plateNumber)) :
                                (x => x.IsAvailableForRent == false && x.PlateNumber.StartsWith(plateNumber))
                            )
                       );


                var res = await service.GetAllByTransactionAsync(pageIndex, pageSize,
                    expression
                    , ["company", "fule", "category"]);
                if (res == null)
                    return NotFound("Not found vehicle");

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }



      

        [HttpGet]
        [Authorize]
        [Route("GetCountVehicle/modes={modes}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  async Task<IActionResult> GetCountVehicle(Modes modes)
        {
            try
            {
                return Ok(await service.CountElement((modes == Modes.All ? null :
                    (modes == Modes.Available ? x => x.IsAvailableForRent == true : x => x.IsAvailableForRent == false))));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }




        [HttpGet]
        [Authorize]
        [Route("IsExistVehicle/plateNumber={plateNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsExistVehicle(string plateNumber)
        {
            if (plateNumber.Length < 1)
                return BadRequest("Enter correct Name");

            try
            {
                return Ok(await service.IsExist(v => v.PlateNumber == plateNumber));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetVehicle/plateNumber={plateNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicleByPlateNumber(string plateNumber)
        {
            if (plateNumber.Length < 1)
                return BadRequest("Enter correct Name");

            try
            {
                var res = await service.Find(v => v.PlateNumber == plateNumber);
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }


        [HttpDelete]
        [Authorize]
        [Route("DeleteVehicle/plateNumber={plateNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVehicleByPlateNumber(string plateNumber)
        {
            if (plateNumber.Length < 1)
                return BadRequest("Enter correct Name");

            try
            {
                var res = await service.Delete(v => v.PlateNumber == plateNumber);
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while get.");
            }
        }


        [HttpPost]
        [Authorize]
        [Route("AddVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddVehicle([FromForm] VehicleDTO dTO)
        {
            if (dTO == null && dTO.imageFile == null)
                return BadRequest("Enter correct Data");

            try
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dTO.imageFile.FileName);
                
                Vehicle vehicle = new()
                {
                    CompanyID = dTO.CompanyID,
                    CategoryID = dTO.CategoryID,
                    FuleID = dTO.FuleID,
                    Model = dTO.Model,
                    Year = dTO.Year,
                    AirCondition = dTO.AirCondition,
                    Doors = dTO.Doors,
                    IsAvailableForRent = true,
                    Mileage = dTO.Mileage,
                    Path = fileName,
                    RentalPricePerDay = dTO.RentalPricePerDay,
                    PlateNumber = dTO.PlateNumber,
                    Seats = dTO.Seats,
                    Transmission = dTO.Transmission
                };
                var res =  await service.AddNew(vehicle);
                if (res == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while post.");

                await ImageService.UploadImageAsync(dTO.imageFile, fileName);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while post.");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVehicle([FromForm] VehicleDTO dTO)
        {
            if (dTO == null)
                return BadRequest("Enter correct Data");

            try
            {
                var vehicleSearch = await service.Find(v => v.PlateNumber == dTO.PlateNumber);
                if (vehicleSearch == null)
                    return BadRequest("Enter correct Data");

                string fileName = "";
                if (dTO.imageFile != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(dTO.imageFile.FileName);
                    if (String.IsNullOrEmpty(await ImageService.UpdateImageAsync(dTO.imageFile, fileName, vehicleSearch.Path)))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while post.");
                    }

                }
                else
                    fileName = vehicleSearch.Path;

                Vehicle vehicle = new()
                {
                    VehicleID = vehicleSearch  .VehicleID,
                    CompanyID = dTO.CompanyID,
                    CategoryID = dTO.CategoryID,
                    FuleID = dTO.FuleID,
                    Model = dTO.Model,
                    Year = dTO.Year,
                    AirCondition = dTO.AirCondition,
                    Doors = dTO.Doors,
                    IsAvailableForRent = true,
                    Mileage = dTO.Mileage,
                    Path = fileName,
                    RentalPricePerDay = dTO.RentalPricePerDay,
                    PlateNumber = dTO.PlateNumber,
                    Seats = dTO.Seats,
                    Transmission = dTO.Transmission
                };

                var res = await service.Update(vehicle, v => v.PlateNumber == dTO.PlateNumber);


                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while post.");
            }
        }
        [HttpGet]
        [Route("GetVehicleImage/FileName={FileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicleImage(string FileName)
        {
            if (FileName.Length < 1)
                return BadRequest("Enter correct Name");

            try
            {
                var uploadDirectory = @"C:\Users\Asus\Desktop\Projects\Car rental\Car rental online\Car rental by EF\Images";
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

        [HttpPost]
        [Route("UploadVehicleImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");

           try
           {
                // Directory where files will be uploaded
                var uploadDirectory = @"C:\Users\Asus\Desktop\Projects\Car rental\Car rental online\Car rental by EF\Images";

                // Generate a unique filename
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadDirectory, fileName);

                // Ensure the uploads directory exists, create if it doesn't
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Return the file path as a response
                return Ok(new { filePath });
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
