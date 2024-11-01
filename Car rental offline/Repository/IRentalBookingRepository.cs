using Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDifferentOperationsRepository
    {
        Task<SubInfoBookingDTO> GetInfoBookingByPlateNumber(string plateNumber);
        Task<bool> AddReturnByPlateNumber(string plateNumber, short ConsumedMilaeage, string FinalCheckNotes);
        
    }
}
