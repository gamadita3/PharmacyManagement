using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]

public class DrugsController : ControllerBase
{
    private readonly InventoryContext _context;
    private readonly ILogger<DrugsController> _logger;

    public DrugsController(InventoryContext context, ILogger<DrugsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //Get all drug
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Drug>>> GetDrugs()
    {
        _logger.LogInformation("Fetching all drugs from the database.");
        return await _context.Drugs.ToListAsync();
    }

    //Get drug by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Drug>> GetDrug(Guid id)
    {
        var drug = await _context.Drugs.FindAsync(id);
        if (drug == null) return NotFound();
        return drug;
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Drug>>> GetDrugsByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Search term cannot be empty.");
        }

        var drugs = await _context.Drugs
            .Where(d => d.Name.Contains(name))
            .ToListAsync();

        if (drugs == null || drugs.Count == 0)
        {
            return NotFound($"No drugs found with name containing '{name}'.");
        }

        return drugs;
    }

    //Add Drug and invoice
    [HttpPost]
    public async Task<ActionResult<Drug>> PostDrug(Drug drug)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(drug.ManufacturerId);
        if (manufacturer == null)
        {
            return BadRequest("Invalid ManufacturerId.");
        }

        _context.Drugs.Add(drug);
        await _context.SaveChangesAsync();

        var invoice = new Invoice
        {
            DrugId = drug.DrugId,
            Quantity = drug.Quantity,
            TotalPrice = drug.Price * drug.Quantity,
            Date = DateTime.UtcNow
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDrug), new { id = drug.DrugId }, drug);
    }

    //Update Drug by id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDrug(Guid id, Drug drug)
    {
        if (id != drug.DrugId) return BadRequest();

        _context.Entry(drug).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //Remove drug
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDrug(Guid id)
    {
        var drug = await _context.Drugs.FindAsync(id);
        if (drug == null) return NotFound();

        _context.Drugs.Remove(drug);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
