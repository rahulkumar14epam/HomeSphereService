namespace HomeSphereService.Models
{
    public class ContactRequest
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public bool IsRead { get; set; }
        public string RequestType { get; set; } = string.Empty;
    }
}