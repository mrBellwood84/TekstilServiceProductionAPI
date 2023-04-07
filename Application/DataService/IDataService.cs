using Models.Production;
using Simulator.Models;

namespace Application.DataProvider
{
    public interface IDataService
    {
        string MachineDataCacheKey { get; }
        string ProductionDataCacheKey { get; }

        List<ProductionData> GetCurrentProductionData();
        List<Machine> GetMachines();
        void UpdateProductionData(ProductionUpdateDTO updateDTO);
    }
}