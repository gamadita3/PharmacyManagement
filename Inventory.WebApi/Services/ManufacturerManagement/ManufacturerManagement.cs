using Inventory.WebApi.Dto;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.WebApi.Services.ManufacturerManagement
{
    public class ManufacturerManagement : IManufacturerManagement
    {
        private readonly InventoryContext _context;

        public ManufacturerManagement(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ManufacturerModel>> GetAllManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<ManufacturerModel> GetManufacturerById(Guid id)
        {
            return await _context.Manufacturers.FindAsync(id);
        }

        public async Task<ManufacturerModel> AddManufacturer(AddManufacturerDto manufacturerDto)
        {
            var manufacturer = new ManufacturerModel
            {
                Name = manufacturerDto.Name,
                Country = manufacturerDto.Country
            };

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            return manufacturer;
        }

        public async Task<ManufacturerModel> UpdateManufacturer(Guid id, UpdateManufacturerDto manufacturerDto)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return null;

            manufacturer.Name = manufacturerDto.Name;
            manufacturer.Country = manufacturerDto.Country;

            _context.Entry(manufacturer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return manufacturer;
        }

        public async Task<bool> RemoveManufacturer(Guid id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return false;

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
