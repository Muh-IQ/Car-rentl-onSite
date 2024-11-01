using Repository;
using Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class DifferentOperationsService(IDifferentOperationsRepository Repository) : IDifferentOperationsRepository
    {
        public async Task<bool> AddReturnByPlateNumber(string plateNumber, short ConsumedMilaeage, string FinalCheckNotes)
        {
            return await Repository.AddReturnByPlateNumber(plateNumber, ConsumedMilaeage, FinalCheckNotes);
        }

        public async Task<SubInfoBookingDTO> GetInfoBookingByPlateNumber(string plateNumber)
        {
            return await Repository.GetInfoBookingByPlateNumber(plateNumber);
        }
    }
}
