using Microsoft.Extensions.Caching.Memory;
using Models.Production;
using Simulator.Models;
using System.Text.Json;

namespace Application.DataProvider
{
    public class DataService : IDataService
    {
        private readonly string machineDataCacheKey = "MachineDataKey";
        private readonly string productionDataCacheKey = "ProductionDataCacheKey";
        private readonly IMemoryCache _cache;


        public DataService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string MachineDataCacheKey { get => machineDataCacheKey; }
        public string ProductionDataCacheKey { get => productionDataCacheKey; }


        public async Task<List<Machine>> GetMachines()
        {
            var machines = _cache.Get<List<Machine>>(machineDataCacheKey);
            if (machines == null)
            {
                machines = await getMachinesFromFile();
                _cache.Set(machineDataCacheKey, machines);
            }
            return machines;
        }

        public async Task<List<ProductionData>> GetCurrentProductionData()
        {
            var productionData = _cache.Get<List<ProductionData>>(productionDataCacheKey);
            if (productionData == null)
            {
                productionData = await createProductionData();
                _cache.Set(productionDataCacheKey, productionData);
            }
            return productionData;
        }

        public async Task UpdateProductionData(ProductionUpdateDTO updateDTO)
        {
            var data = await GetCurrentProductionData();
            var updateIndex = data.FindIndex(x => x.Machine.Id == updateDTO.MachineId);
            data[updateIndex].CurrentValue = updateDTO.Value;
            _cache.Set(productionDataCacheKey, data);
        }

        private async Task<List<Machine>> getMachinesFromFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(path, "machines.json");
            bool fileExist = File.Exists(file);

            if (fileExist)
            {
                using FileStream openStream = File.OpenRead(file);
                var fileData = await JsonSerializer.DeserializeAsync<List<Machine>>(openStream);
                if (fileData != null) return fileData;
                return new List<Machine>();
            }

            var newData = createMachines();
            using FileStream createStream = File.Create(file);
            await JsonSerializer.SerializeAsync(createStream, newData);
            return newData;
        }

        private List<Machine> createMachines()
        {
            var machines = new List<Machine>();

            for (int i = 0; i < 6; i++)
            {
                var machine = new Machine
                {
                    Id = Guid.NewGuid(),
                    Name = $"machine-{i+1}",
                    DisplayName = $"Stasjon {i+1}",
                    MachineType = "Test",
                };
                machines.Add(machine);
            }

            return machines;
        }

        private async Task<List<ProductionData>> createProductionData()
        {
            var machines = _cache.Get<List<Machine>>(machineDataCacheKey);
            if (machines == null)
            {
                machines = await getMachinesFromFile();
                _cache.Set(machineDataCacheKey, machines);
            }

            var now = DateTime.Now;
            var dateOnly = new DateOnly(now.Year, now.Month, now.Day);

            var currentProduction = new List<ProductionData>();

            foreach (var machine in machines)
            {
                var data = new ProductionData
                {
                    Id = Guid.NewGuid(),
                    Machine = machine,
                    Date = dateOnly,
                    CurrentValue = 0,
                    YellowTarget = 2400,
                    GreenTarget = 3600,
                    MaxTarget = 6000,
                };
                currentProduction.Add(data);
            }

            return currentProduction;
        }
    }
}
