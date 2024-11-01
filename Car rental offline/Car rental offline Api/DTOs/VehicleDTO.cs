using Data.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_rental_offline_Api.DTOs
{
    public class VehicleDTO
    {
        public int CompanyID { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int FuleID { get; set; }
        public string PlateNumber { get; set; }
        public int CategoryID { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public bool Transmission { get; set; }
        public IFormFile? imageFile { get; set; } = null;
        public Byte Doors { get; set; }
        public Byte Seats { get; set; }
        public bool AirCondition { get; set; }


    }
}
