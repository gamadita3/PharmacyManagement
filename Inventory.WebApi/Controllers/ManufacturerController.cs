using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly InventoryContext _context;

        public ManufacturersController(InventoryContext context)
        {
            _context = context;
        }

        //Get manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        //Get specific manufacturer
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(Guid id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return NotFound();

            return manufacturer;
        }

        //Add manufacturer
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(manufacturer.Name))
            {
                return BadRequest("Manufacturer name is required.");
            }

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetManufacturer), new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        //Update manufacturer
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(Guid id, Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId) return BadRequest();

            _context.Entry(manufacturer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Remove manufacturer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return NotFound();

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
