using HomeSphereService.Models;

namespace HomeSphereService.Dto
{
    public static class SampleData
    {
        public static List<Property> GetSampleProperties()
        {
            return new List<Property>
            {
                new Property
                {
                    Id = 1,
                    Title = "Luxury Villa in Lutyens' Delhi",
                    Description = "Experience opulent living in this grand 5BHK villa located in the prestigious Lutyens' Delhi. Features lush gardens, a private pool, and state-of-the-art amenities. Perfect for those seeking exclusivity and comfort in the heart of the capital.",
                    Price = 120_000_000M, // ?12 Crore
                    Location = "Lutyens' Delhi, New Delhi, Delhi",
                    PropertyType = "Villa",
                    Bedrooms = 5,
                    Bathrooms = 5,
                    SquareFootage = 9000,
                    ViewCount = 0,
                    DateListed = DateTime.UtcNow.AddDays(-12),
                    ImageUrls = new List<string>
                    {
                        "https://example.com/images/delhi-villa-1.jpg",
                        "https://example.com/images/delhi-villa-2.jpg"
                    },
                    AgentName = "Rohit Sharma",
                    AgentEmail = "rohit.sharma@luxuryrealty.in",
                    AgentPhone = "+91-9876543210",
                    IsActive = true
                },
                new Property
                {
                    Id = 2,
                    Title = "Modern Apartment in Bandra Kurla Complex",
                    Description = "A stylish 2BHK apartment in Mumbai's BKC, close to business hubs, shopping, and nightlife. Features modern interiors, 24/7 security, and a city view balcony. Ideal for professionals and small families.",
                    Price = 65_000_000M, // ?6.5 Crore
                    Location = "Bandra Kurla Complex, Mumbai, Maharashtra",
                    PropertyType = "Apartment",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    SquareFootage = 1300,
                    ViewCount = 0,
                    DateListed = DateTime.UtcNow.AddDays(-20),
                    ImageUrls = new List<string>
                    {
                        "https://example.com/images/mumbai-apartment-1.jpg"
                    },
                    AgentName = "Priya Mehta",
                    AgentEmail = "priya.mehta@cityhomes.in",
                    AgentPhone = "+91-9988776655",
                    IsActive = true
                },
                new Property
                {
                    Id = 3,
                    Title = "Spacious Suburban Home in Whitefield",
                    Description = "A beautiful 3BHK independent house in Whitefield, Bengaluru. Enjoy a peaceful neighborhood, private garden, and easy access to IT parks and schools. Perfect for growing families.",
                    Price = 18_000_000M, // ?1.8 Crore
                    Location = "Whitefield, Bengaluru, Karnataka",
                    PropertyType = "Independent House",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    SquareFootage = 2500,
                    ViewCount = 0,
                    DateListed = DateTime.UtcNow.AddDays(-7),
                    ImageUrls = new List<string>
                    {
                        "https://example.com/images/bangalore-home-1.jpg"
                    },
                    AgentName = "Anil Kumar",
                    AgentEmail = "anil.kumar@suburbanrealty.in",
                    AgentPhone = "+91-9123456789",
                    IsActive = true
                },
                new Property
                {
                    Id = 4,
                    Title = "Beachfront Condo in North Goa",
                    Description = "Wake up to the sound of waves in this 2BHK beachfront condo at Candolim Beach. Modern interiors, a sea-facing balcony, and access to private beach amenities make this a perfect holiday home.",
                    Price = 30_000_000M, // ?3 Crore
                    Location = "Candolim Beach, North Goa, Goa",
                    PropertyType = "Condo",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    SquareFootage = 1400,
                    ViewCount = 0,
                    DateListed = DateTime.UtcNow.AddDays(-15),
                    ImageUrls = new List<string>
                    {
                        "https://example.com/images/goa-condo-1.jpg"
                    },
                    AgentName = "Sonia D'Souza",
                    AgentEmail = "sonia.dsouza@beachlife.in",
                    AgentPhone = "+91-9876501234",
                    IsActive = true
                },
                new Property
                {
                    Id = 5,
                    Title = "Family Home in Gachibowli",
                    Description = "A perfect 4BHK family home in Gachibowli, Hyderabad. Spacious rooms, a modern kitchen, and proximity to top schools and IT companies make this ideal for families.",
                    Price = 9_500_000M, // ?95 Lakh
                    Location = "Gachibowli, Hyderabad, Telangana",
                    PropertyType = "House",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    SquareFootage = 2200,
                    ViewCount = 0,
                    DateListed = DateTime.UtcNow.AddDays(-2),
                    ImageUrls = new List<string>
                    {
                        "https://example.com/images/hyderabad-home-1.jpg"
                    },
                    AgentName = "Vikram Singh",
                    AgentEmail = "vikram.singh@familyhomes.in",
                    AgentPhone = "+91-9000012345",
                    IsActive = true
                }
            };
        }
    }
}