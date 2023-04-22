using Models.Production;
using Simulator.Models;

namespace Application.DataProvider
{
    public interface IDataService
    {
        string MachineDataCacheKey { get; }
        string ProductionDataCacheKey { get; }

        Task<List<ProductionData>> GetCurrentProductionData();
        Task<List<Machine>> GetMachines();
        Task UpdateProductionData(ProductionUpdateDTO updateDTO);
    }
}