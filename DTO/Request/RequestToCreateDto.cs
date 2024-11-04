namespace MIRSAL.DTO
{
    public class RequestToCreateDto
    {
        public string CustomerID { get; set; } = null!;
        public string PolicyID { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string IncidentStartTime { get; set; } = null!;
        public string IncidentEndTime { get; set; } = null!;
        public string Description {get; set; } = null!; 
        public DateTime IncidentDate { get; set; }
    }
}