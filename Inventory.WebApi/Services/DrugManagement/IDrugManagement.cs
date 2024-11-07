using Inventory.WebApi.Dto;
using Inventory.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.WebApi.Services.DrugManagement
{
    public interface IDrugManagement
    {
        Task<IEnumerable<DrugModel>> GetAllDrugs();
        Task<DrugModel> GetDrugById(Guid id);
        Task<IEnumerable<DrugModel>> SearchDrugsByName(string name);
        Task<DrugModel> AddDrug(AddDrugDto drugDto);
        Task<DrugModel> UpdateDrug(Guid id, UpdateDrugDto drugDto);
        Task<bool> RemoveDrug(Guid id);
    }
}
