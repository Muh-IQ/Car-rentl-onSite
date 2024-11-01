using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class VehicleReturn
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime ActualReturnDate { get; set; }

        [Required]
        public short ActualRentalDays { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public short ConsumedMilaeage { get; set; }

        [Required, MaxLength(500)]
        public string FinalCheckNotes { get; set; }

        [Required]
        public decimal AdditionalCharges { get; set; }
        [Required]
        public decimal ActualTotalDueAmount { get; set; }
    }
}
