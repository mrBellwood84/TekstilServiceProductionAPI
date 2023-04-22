using Simulator.Models;
using System.Net.Http.Json;

namespace Simulator.Lib
{
    internal static class SimulatorHttpClient
    {
        internal static async Task<List<ProductionData>> GetProductionData()
        {
            try
            {
                var client = new HttpClient();
                var result = await client.GetAsync("https://localhost:7031/api/production");
                if (result.IsSuccessStatusCode)
                {
                    var machines = await result.Content.ReadFromJsonAsync<List<ProductionData>>();
                    return machines;
                }
                return new List<ProductionData>();
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
