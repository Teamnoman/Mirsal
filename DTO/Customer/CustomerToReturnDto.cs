namespace MIRSAL.DTO
{
    public class CustomerToReturnDto
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}