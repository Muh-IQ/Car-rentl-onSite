using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.DTOs;

namespace Data.ImplementeRepository
{
    public class ImplementeDifferentOperationsRepository(AppDbContext context) : IDifferentOperationsRepository
    {
        public async Task<bool> AddReturnByPlateNumber(string plateNumber, short ConsumedMilaeage, string FinalCheckNotes)
        {
            /*
            
            ### Algorithm

                **Goal**: Add data to the `VehicleReturn` table and update data in the `RentalTransaction` table.

            **Execution**:
            
                1. Search for vehicle data in the `Vehicle` table using the `plateNumber`.
                2. Search in the `RentalBooking` table for the last booking record that matches the vehicle ID obtained from the 
                   `Vehicle` table (this is done to handle cases where the vehicle may have been booked and returned in previous transactions).
                3. Insert the required data into the `VehicleReturn` table.
                4. Update the `RentalTransaction` table and add the necessary data.
                5. Update the Mileage in the Vehicle table by adding the previous Mileage to the ConsumedMilaeage,
                    and mark the vehicle as available for rental in the Vehicle table
             */


            if (plateNumber.Length < 1)
                return false;

            // بدء المعاملة
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                //get vehicle info 
                var vehicle = await context.Vehicle.FirstOrDefaultAsync(v => v.PlateNumber == plateNumber);
                if (vehicle == null)
                    return false;

                var booking = await context.RentalBooking.OrderByDescending(i => i.ID)
                    .FirstOrDefaultAsync(rb => rb.VehicleID == vehicle.VehicleID);
                if (booking == null)
                    return false;

                /*
                 Calculate the difference between the current time and the booking end date.
                 The result can be either negative or positive.
                 (If it's negative, it means we owe money, for example, 
                 if the car rental starts on the 1st of the month and ends on the 10th,
                 but the car is returned on the 7th, we owe the customer for 3 days, so we refund them the money.)
                 */
                        short daysDifference = (short)((DateTime.Now.Date - booking.RentalEndDate.Date ).TotalDays);                        /*
                        Here we calculate if we need to charge the customer money because they may 
                        have exceeded the booking period. For example, if the booking was for 10 days and the
                        customer returns the vehicle after 12 days, we need to charge them for the extra 2 days.
                         */
                        decimal AdditionalCharges = daysDifference > 0 ? booking.RentalPricePerDay * daysDifference : 0;
                        decimal ActualTotalDueAmountDifference = daysDifference < 0 ? booking.RentalPricePerDay * daysDifference : 0;
                //

                // add VehicleReturn
                VehicleReturn vehicleReturn = new()
                {
                    ActualReturnDate = DateTime.Now,
                    ActualRentalDays = (short)((DateTime.Now - booking.RentalStartDate).Days),
                    Mileage = vehicle.Mileage,
                    ConsumedMilaeage = ConsumedMilaeage,
                    FinalCheckNotes = FinalCheckNotes,
                    AdditionalCharges = AdditionalCharges,
                    ActualTotalDueAmount = (ActualTotalDueAmountDifference != 0 ? ActualTotalDueAmountDifference : AdditionalCharges)
                                           + booking.InitialTotalDueAmount
                };

                await context.VehicleReturn.AddAsync(vehicleReturn);
                await context.SaveChangesAsync();

                //update RentalTransaction
                var rentalTransaction = await context.RentalTransaction.FirstOrDefaultAsync(v => v.RentalBookingID == booking.ID);
                if (rentalTransaction == null)
                    return false;

                rentalTransaction.VehicleReturnID = vehicleReturn.ID;
                rentalTransaction.ActualTotalDueAmount = vehicleReturn.ActualTotalDueAmount;
                rentalTransaction.TotalRefundedAmount = ActualTotalDueAmountDifference * -1; //convert it to positive
                rentalTransaction.TotalRemaining = 0;
                rentalTransaction.UpdatedTransactionDate = DateTime.Now;

                //update Mileage ,IsAvailableForRent in Vehicle table
                vehicle.Mileage += ConsumedMilaeage;
                vehicle.IsAvailableForRent = true;

                await context.SaveChangesAsync();

                // تأكيد المعاملة
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                // إلغاء المعاملة في حالة حدوث خطأ
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<SubInfoBookingDTO> GetInfoBookingByPlateNumber(string plateNumber)
        {
            if (plateNumber.Length < 1)
                return new SubInfoBookingDTO();

            var vehicle = await context.Vehicle.FirstOrDefaultAsync(v => v.PlateNumber == plateNumber);

            if (vehicle == null)
                return new SubInfoBookingDTO();

            
            var booking = await context.RentalBooking.OrderByDescending(i => i.ID).Select
                (r => new
                {
                    r.VehicleID,
                    r.RentalEndDate,
                    r.InitialTotalDueAmount,
                    r.RentalStartDate,
                    r.RentalPricePerDay
                }

                ).FirstOrDefaultAsync( rb => rb.VehicleID == vehicle.VehicleID);
            return new SubInfoBookingDTO()
            {
                Mileage = vehicle.Mileage,
                RentalEndDate = booking.RentalEndDate.ToShortDateString(),
                InitialTotalDueAmount = booking.InitialTotalDueAmount,
                RentalStartDate = booking.RentalStartDate.ToShortDateString(),
                RentalPricePerDay = booking.RentalPricePerDay
            };
        }
    }
}