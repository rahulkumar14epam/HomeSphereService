using HomeSphereService.Models;

namespace HomeSphereService.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly List<Property> _properties = new();
        private readonly List<ContactRequest> _contactRequests = new();
        private int _propertyIdCounter = 1;
        private int _contactRequestIdCounter = 1;

        public Task<IEnumerable<PropertySummaryDto>> GetAllPropertiesAsync(PropertyFilter? filters = null, PropertySortOrder? sorting = null)
        {
            var query = _properties.AsQueryable();

            // Filtering
            if (filters != null)
            {
                if (!string.IsNullOrWhiteSpace(filters.Location))
                    query = query.Where(p => p.Location.Contains(filters.Location, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrWhiteSpace(filters.PropertyType))
                    query = query.Where(p => p.PropertyType.Equals(filters.PropertyType, StringComparison.OrdinalIgnoreCase));
                if (filters.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= filters.MinPrice.Value);
                if (filters.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= filters.MaxPrice.Value);
                if (filters.MinBedrooms.HasValue)
                    query = query.Where(p => p.Bedrooms >= filters.MinBedrooms.Value);
                if (filters.MaxBedrooms.HasValue)
                    query = query.Where(p => p.Bedrooms <= filters.MaxBedrooms.Value);
                if (filters.IsActive.HasValue)
                    query = query.Where(p => p.IsActive == filters.IsActive.Value);
            }

            // Sorting
            if (sorting != null)
            {
                query = sorting switch
                {
                    PropertySortOrder.PriceAsc => query.OrderBy(p => p.Price),
                    PropertySortOrder.PriceDesc => query.OrderByDescending(p => p.Price),
                    PropertySortOrder.DateListedAsc => query.OrderBy(p => p.DateListed),
                    PropertySortOrder.DateListedDesc => query.OrderByDescending(p => p.DateListed),
                    PropertySortOrder.ViewCountDesc => query.OrderByDescending(p => p.ViewCount),
                    _ => query
                };
            }

            IEnumerable<PropertySummaryDto> result = query.Select(p => new PropertySummaryDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Location = p.Location,
                PropertyType = p.PropertyType,
                Bedrooms = p.Bedrooms,
                Bathrooms = p.Bathrooms,
                ThumbnailUrl = p.ImageUrls.FirstOrDefault() ?? string.Empty,
                ViewCount = p.ViewCount
            });

            return Task.FromResult(result);
        }

        public Task<Property?> GetPropertyByIdAsync(int id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(property);
        }

        public Task<Property> CreatePropertyAsync(CreatePropertyDto createDto)
        {
            var property = new Property
            {
                Id = _propertyIdCounter++,
                Title = createDto.Title,
                Description = createDto.Description,
                Price = createDto.Price,
                Location = createDto.Location,
                PropertyType = createDto.PropertyType,
                Bedrooms = createDto.Bedrooms,
                Bathrooms = createDto.Bathrooms,
                SquareFootage = createDto.SquareFootage,
                ViewCount = 0,
                DateListed = DateTime.UtcNow,
                ImageUrls = createDto.ImageUrls,
                AgentName = createDto.AgentName,
                AgentEmail = createDto.AgentEmail,
                AgentPhone = createDto.AgentPhone,
                IsActive = createDto.IsActive
            };
            _properties.Add(property);
            return Task.FromResult(property);
        }

        public Task<Property?> UpdatePropertyAsync(int id, CreatePropertyDto updateDto)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property == null) return Task.FromResult<Property?>(null);

            property.Title = updateDto.Title;
            property.Description = updateDto.Description;
            property.Price = updateDto.Price;
            property.Location = updateDto.Location;
            property.PropertyType = updateDto.PropertyType;
            property.Bedrooms = updateDto.Bedrooms;
            property.Bathrooms = updateDto.Bathrooms;
            property.SquareFootage = updateDto.SquareFootage;
            property.ImageUrls = updateDto.ImageUrls;
            property.AgentName = updateDto.AgentName;
            property.AgentEmail = updateDto.AgentEmail;
            property.AgentPhone = updateDto.AgentPhone;
            property.IsActive = updateDto.IsActive;

            return Task.FromResult<Property?>(property);
        }

        public Task<bool> DeletePropertyAsync(int id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property == null) return Task.FromResult(false);
            _properties.Remove(property);
            return Task.FromResult(true);
        }

        public Task IncrementViewCountAsync(int id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property != null)
            {
                property.ViewCount++;
            }
            return Task.CompletedTask;
        }

        public Task<ContactRequest> SubmitContactRequestAsync(ContactRequest contactRequest)
        {
            contactRequest.Id = _contactRequestIdCounter++;
            contactRequest.RequestDate = DateTime.UtcNow;
            contactRequest.IsRead = false;
            _contactRequests.Add(contactRequest);
            return Task.FromResult(contactRequest);
        }
    }

    public class PropertyFilter
    {
        public string? Location { get; set; }
        public string? PropertyType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MaxBedrooms { get; set; }
        public bool? IsActive { get; set; }
    }

    public enum PropertySortOrder
    {
        PriceAsc,
        PriceDesc,
        DateListedAsc,
        DateListedDesc,
        ViewCountDesc
    }
}