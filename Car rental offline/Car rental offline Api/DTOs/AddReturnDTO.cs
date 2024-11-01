namespace Car_rental_offline_Api.DTOs
{
    public class AddReturnDTO
    {
        public required string plateNumber {  get; set; }
        public required short ConsumedMilaeage { get; set; }
        public  string FinalCheckNotes { get; set; } = string.Empty;
    }
}
