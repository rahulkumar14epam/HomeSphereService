namespace HomeSphereService.Models
{
    public class CreatePropertyDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareFootage { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public string AgentName { get; set; } = string.Empty;
        public string AgentEmail { get; set; } = string.Empty;
        public string AgentPhone { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}