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

    //Get invoices
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
    {
        return await _context.Invoices
            .Include(i => i.Drug)
            .ToListAsync();
    }

    //Get invoice
    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Drug)
            .FirstOrDefaultAsync(i => i.InvoiceId == id);

        if (invoice == null) return NotFound();

        return invoice;
    }
}
