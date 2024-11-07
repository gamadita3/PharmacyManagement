using Inventory.WebApi.Dto;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.Services.ManufacturerManagement
{
    public interface IManufacturerManagement
    {
        Task<IEnumerable<ManufacturerModel>> GetAllManufacturers();
        Task<ManufacturerModel> GetManufacturerById(Guid id);
        Task<ManufacturerModel> AddManufacturer(AddManufacturerDto manufacturerDto);
        Task<ManufacturerModel> UpdateManufacturer(Guid id, UpdateManufacturerDto manufacturerDto);
        Task<bool> RemoveManufacturer(Guid id);
    }
}
