using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly InventoryContext _context;

    public InvoicesController(InventoryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceModel>>> GetInvoices()
    {
        return await _context.Invoices
            .Include(i => i.Drug)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceModel>> GetInvoice(Guid id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Drug)
            .FirstOrDefaultAsync(i => i.InvoiceId == id);

        if (invoice == null) return NotFound();

        return invoice;
    }
}
