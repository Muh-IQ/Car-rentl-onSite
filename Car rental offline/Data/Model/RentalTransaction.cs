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
    public class RentalTransaction
    {
        [Key]
        public int ID { get; set; }

        // Foreign key for Rental Booking
        [ForeignKey("RentalBookingID")]
        public int RentalBookingID { get; set; }
        public RentalBooking rentalBooking { get; set; }

        // Foreign key for Rental Booking
        [ForeignKey("VehicleReturnID")]
        public int? VehicleReturnID { get; set; }
        public VehicleReturn vehicleReturn { get; set; }

        [Required, MaxLength(20)]
        public string PaymentCardNumber { get; set; }

        [Required]
        public decimal PaidInitialTotalDueAmount { get; set; }
        public decimal ActualTotalDueAmount { get; set; }
        public decimal TotalRemaining { get; set; }
        public decimal TotalRefundedAmount { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        public DateTime UpdatedTransactionDate { get; set; }
    }

}
