namespace MIRSAL.DTO
{
    public class RequestPath
    {
        public Coordinates StartPoint { get; set; } = null!;
        public List<Coordinates> MiddlePoints { get; set; } = null!;
        public Coordinates EndPoint { get; set; } = null!;
    }

    public class Coordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}