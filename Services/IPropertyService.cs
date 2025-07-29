using HomeSphereService.Models;

namespace HomeSphereService.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertySummaryDto>> GetAllPropertiesAsync(PropertyFilter? filters = null, PropertySortOrder? sorting = null);
        Task<Property?> GetPropertyByIdAsync(int id);
        Task<Property> CreatePropertyAsync(CreatePropertyDto createDto);
        Task<Property?> UpdatePropertyAsync(int id, CreatePropertyDto updateDto);
        Task<bool> DeletePropertyAsync(int id);
        Task IncrementViewCountAsync(int id);
        Task<ContactRequest> SubmitContactRequestAsync(ContactRequest contactRequest);
    }
}