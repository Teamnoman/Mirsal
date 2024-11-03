using MIRSAL.Models;

namespace MIRSAL.DTO
{
    public class DashboardDto
    {
        public Customer CustomerInfo { get; set; } = null!;
        public Request RequestInfo { get; set; } = null!;
        public Policy PolicyInfo { get; set; } = null!;
        public Vehicle VehicleInfo { get; set; } = null!;
    }
}