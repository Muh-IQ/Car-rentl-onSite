using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class RentalBooking 
    {
        [Key]
        public int ID { get; set; }

        //Foreign key for Customer
        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }
        public Customer customer { get; set; }

        // Foreign key for Vehicle
        [ForeignKey("VehicleID")]
        public int VehicleID { get; set; }
        public Vehicle vehicle { get; set; }

        [Required]
        public DateTime RentalStartDate { get; set; }
        [Required]
        public DateTime RentalEndDate { get; set; }
        [Required]
        public int InitialRentalDays { get; set; }
        [Required]
        public decimal RentalPricePerDay { get; set; }
        [Required]
        public decimal InitialTotalDueAmount { get; set; }

        [Required, MaxLength(500)]
        public string Notes { get; set; }
    }
}
