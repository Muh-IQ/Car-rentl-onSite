using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs
{
    public class SubInfoBookingDTO
    {
        public int Mileage { get; set; }
        public string RentalStartDate { get; set; }
        public string RentalEndDate { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public decimal InitialTotalDueAmount { get; set; }
    }
}
