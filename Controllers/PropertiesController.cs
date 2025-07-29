using System.Collections.Concurrent;
using HomeSphereService.Dto;
using HomeSphereService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeSphereService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        // In-memory thread-safe storage
        private static readonly ConcurrentBag<Property> Properties = new ConcurrentBag<Property>(SampleData.GetSampleProperties());
        private static int _propertyIdCounter = 1;
        private static readonly ConcurrentBag<ContactRequest> ContactRequests = new();
        private static int _contactRequestIdCounter = 1;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertySummaryDto>>> GetAll(
            [FromQuery] string? location,
            [FromQuery] string? propertyType,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? bedrooms,
            [FromQuery] int? bathrooms,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortOrder)
        {
            var query = Properties.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(p => p.Location.Contains(location, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(propertyType))
                query = query.Where(p => p.PropertyType.Equals(propertyType, StringComparison.OrdinalIgnoreCase));
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);
            if (bedrooms.HasValue)
                query = query.Where(p => p.Bedrooms == bedrooms.Value);
            if (bathrooms.HasValue)
                query = query.Where(p => p.Bathrooms == bathrooms.Value);

            // Sorting
            bool desc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);
            query = sortBy?.ToLower() switch
            {
                "price" => desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "datelisted" => desc ? query.OrderByDescending(p => p.DateListed) : query.OrderBy(p => p.DateListed),
                "viewcount" => desc ? query.OrderByDescending(p => p.ViewCount) : query.OrderBy(p => p.ViewCount),
                _ => query
            };

            var result = query.Select(p => new PropertySummaryDto
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

            return await Task.FromResult(Ok(result));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Property>> GetById(int id)
        {
            var property = Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return await Task.FromResult(NotFound());
            return await Task.FromResult(Ok(property));
        }

        [HttpPost]
        public async Task<ActionResult<Property>> Create([FromBody] CreatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = new Property
            {
                Id = _propertyIdCounter++,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Location = dto.Location,
                PropertyType = dto.PropertyType,
                Bedrooms = dto.Bedrooms,
                Bathrooms = dto.Bathrooms,
                SquareFootage = dto.SquareFootage,
                ViewCount = 0,
                DateListed = DateTime.UtcNow,
                ImageUrls = dto.ImageUrls,
                AgentName = dto.AgentName,
                AgentEmail = dto.AgentEmail,
                AgentPhone = dto.AgentPhone,
                IsActive = dto.IsActive
            };
            Properties.Add(property);
            return await Task.FromResult(CreatedAtAction(nameof(GetById), new { id = property.Id }, property));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Property>> Update(int id, [FromBody] CreatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return await Task.FromResult(NotFound());

            property.Title = dto.Title;
            property.Description = dto.Description;
            property.Price = dto.Price;
            property.Location = dto.Location;
            property.PropertyType = dto.PropertyType;
            property.Bedrooms = dto.Bedrooms;
            property.Bathrooms = dto.Bathrooms;
            property.SquareFootage = dto.SquareFootage;
            property.ImageUrls = dto.ImageUrls;
            property.AgentName = dto.AgentName;
            property.AgentEmail = dto.AgentEmail;
            property.AgentPhone = dto.AgentPhone;
            property.IsActive = dto.IsActive;

            return await Task.FromResult(Ok(property));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return await Task.FromResult(NotFound());

            var removed = Properties.TryTake(out var removedProperty);
            // If TryTake fails, fallback to manual removal for safety
            if (!removed)
            {
                var list = Properties.ToList();
                var toRemove = list.FirstOrDefault(p => p.Id == id);
                if (toRemove != null)
                {
                    list.Remove(toRemove);
                    // Rebuild the bag
                    while (Properties.TryTake(out _)) { }
                    foreach (var p in list)
                        Properties.Add(p);
                }
            }
            return await Task.FromResult(NoContent());
        }

        [HttpPost("{id:int}/contact")]
        public async Task<ActionResult<ContactRequest>> SubmitContactRequest(int id, [FromBody] ContactRequest request)
        {
            var property = Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return await Task.FromResult(NotFound());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Id = _contactRequestIdCounter++;
            request.PropertyId = id;
            request.RequestDate = DateTime.UtcNow;
            request.IsRead = false;
            ContactRequests.Add(request);

            return await Task.FromResult(Ok(request));
        }

        [HttpGet("{id:int}/increment-view")]
        public async Task<IActionResult> IncrementView(int id)
        {
            var property = Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return await Task.FromResult(NotFound());

            property.ViewCount++;
            return await Task.FromResult(NoContent());
        }
    }
}