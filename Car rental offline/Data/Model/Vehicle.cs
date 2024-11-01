using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Model
{
    public class Vehicle 
    {
        [Key]
        public int VehicleID { get; set; }

        // Foreign key for Company
        [ForeignKey("CompanyID")]
        public int CompanyID { get; set; }
        public Company company { get; set; }

        [Required, MaxLength(50)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }
        [Required]
        public int Mileage { get; set; }


        // Foreign key for Fuel
        [ForeignKey("FuleID")]
        public int FuleID { get; set; }
        public Fule fule { get; set; }



        [Required, MaxLength(50)]
        public string PlateNumber { get; set; }


        // Foreign key for Category
        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }
        public Category category { get; set; }


        [Required]
        public decimal RentalPricePerDay { get; set; }

        [Required]
        public bool IsAvailableForRent { get; set; }
        
        [Required]
        public bool Transmission { get; set; }

        [Required, MaxLength(500)]
        public string Path { get; set; }
        
        [Required]
        public Byte Doors { get; set; }
        [Required]
        public Byte Seats { get; set; }

        [Required]
        public bool AirCondition { get; set; }


    }
}
