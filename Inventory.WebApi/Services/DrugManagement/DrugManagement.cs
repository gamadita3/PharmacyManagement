using Inventory.WebApi.Dto;
using Inventory.WebApi.EntityFramework;
using Inventory.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.WebApi.Services.DrugManagement
{
    public class DrugManagement : IDrugManagement
    {
        private readonly InventoryContext _context;

        public DrugManagement(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DrugModel>> GetAllDrugs()
        {
            return await _context.Drugs.ToListAsync();
        }

        public async Task<DrugModel> GetDrugById(Guid id)
        {
            return await _context.Drugs.FindAsync(id);
        }

        public async Task<IEnumerable<DrugModel>> SearchDrugsByName(string name)
        {
            return await _context.Drugs
                .Where(d => d.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<DrugModel> AddDrug(AddDrugDto drugDto)
        {
            var drug = new DrugModel
            {
                Name = drugDto.Name,
                Quantity = drugDto.Quantity,
                Price = drugDto.Price,
                ExpiryDate = drugDto.ExpiryDate,
                ManufacturerId = drugDto.ManufacturerId
            };

            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();

            var invoice = new InvoiceModel
            {
                DrugId = drug.DrugId,
                Quantity = drug.Quantity,
                TotalPrice = drug.Price * drug.Quantity,
                Date = DateTime.UtcNow
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return drug;
        }

        public async Task<DrugModel> UpdateDrug(Guid id, UpdateDrugDto drugDto)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null) return null;

            drug.Name = drugDto.Name;
            drug.Quantity = drugDto.Quantity;
            drug.Price = drugDto.Price;
            drug.ExpiryDate = drugDto.ExpiryDate;
            drug.ManufacturerId = drugDto.ManufacturerId;

            _context.Entry(drug).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return drug;
        }

        public async Task<bool> RemoveDrug(Guid id)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null) return false;

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
