namespace HomeSphereService.Models
{
    public class PropertySummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int ViewCount { get; set; }
    }
}