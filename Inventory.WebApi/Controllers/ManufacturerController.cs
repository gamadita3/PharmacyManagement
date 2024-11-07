using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;
using Inventory.WebApi.Services.ManufacturerManagement;
using Inventory.WebApi.Dto;

namespace Inventory.WebApi.Controllers
{
        [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerManagement _manufacturerService;

        public ManufacturersController(IManufacturerManagement manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerModel>>> GetManufacturers()
        {
            return Ok(await _manufacturerService.GetAllManufacturers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerModel>> GetManufacturer(Guid id)
        {
            var manufacturer = await _manufacturerService.GetManufacturerById(id);
            if (manufacturer == null) return NotFound();

            return manufacturer;
        }

        [HttpPost]
        public async Task<ActionResult<ManufacturerModel>> PostManufacturer(AddManufacturerDto manufacturerDto)
        {
            if (string.IsNullOrWhiteSpace(manufacturerDto.Name))
            {
                return BadRequest("Manufacturer name is required.");
            }

            var createdManufacturer = await _manufacturerService.AddManufacturer(manufacturerDto);
            return CreatedAtAction(nameof(GetManufacturer), new { id = createdManufacturer.ManufacturerId }, createdManufacturer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(Guid id, UpdateManufacturerDto manufacturerDto)
        {
            if (id != manufacturerDto.ManufacturerId) return BadRequest();

            var updatedManufacturer = await _manufacturerService.UpdateManufacturer(id, manufacturerDto);
            if (updatedManufacturer == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveManufacturer(Guid id)
        {
            var deleted = await _manufacturerService.RemoveManufacturer(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
