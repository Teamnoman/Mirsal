namespace MIRSAL.DTO
{
    public class PolicyVehicleToReturndDto
    {
        public string PolicyID { get; set; } = null!;
        public string VehicleModel { get; set; } = null!;
        public int VehicleYear { get; set; }
        public string RegistrationNumber { get; set; } = null!;
    }
}