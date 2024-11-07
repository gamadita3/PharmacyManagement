using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;
using Microsoft.Extensions.Logging;
using Inventory.WebApi.Services.DrugManagement;
using Inventory.WebApi.Dto;

[Route("api/[controller]")]
[ApiController]

public class DrugsController : ControllerBase
{
    private readonly IDrugManagement _drugService;

    public DrugsController(IDrugManagement drugService)
    {
        _drugService = drugService;
    }

    // GET: api/drugs - Get all drugs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DrugModel>>> GetDrugs()
    {
        return Ok(await _drugService.GetAllDrugs());
    }

    // GET: api/drugs/{id} - Get a specific drug by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<DrugModel>> GetDrug(Guid id)
    {
        var drug = await _drugService.GetDrugById(id);
        if (drug == null) return NotFound();

        return drug;
    }

    // GET: api/drugs/search?name={name} - Search for drugs by name
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<DrugModel>>> SearchDrugsByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Search term cannot be empty.");
        }

        var drugs = await _drugService.SearchDrugsByName(name);
        if (drugs == null || !drugs.Any())
        {
            return NotFound($"No drugs found with name containing '{name}'.");
        }

        return Ok(drugs);
    }

    // POST: api/drugs - Add a new drug
    [HttpPost]
    public async Task<ActionResult<DrugModel>> PostDrug(AddDrugDto drug)
    {
        var createdDrug = await _drugService.AddDrug(drug);
        return CreatedAtAction(nameof(GetDrug), new { id = createdDrug.DrugId }, createdDrug);
    }

    // PUT: api/drugs/{id} - Update an existing drug
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDrug(Guid id, UpdateDrugDto drug)
    {
        if (id != drug.DrugId) return BadRequest();

        var updatedDrug = await _drugService.UpdateDrug(id, drug);
        if (updatedDrug == null) return NotFound();

        return NoContent();
    }

    // DELETE: api/drugs/{id} - Delete a drug
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDrug(Guid id)
    {
        var deleted = await _drugService.RemoveDrug(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
