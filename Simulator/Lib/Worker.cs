using Grpc.Core;
using Simulator.Models;

namespace Simulator.Lib
{
    internal class Worker
    {
        private readonly ProductionData _productionData;

        internal Worker(ProductionData productionData)
        {
            _productionData = productionData;
        }

        internal async void Tick()
        {
            var random = new Random();

            for (int i = 0; i < _productionData.MaxTarget; i++)
            {
                int sleep = random.Next(2, 5) * 1000;
                Thread.Sleep(sleep);
                _productionData.CurrentValue++;

                bool result;
                try
                {
                    result = await gRPC_Client.Send(_productionData.Machine.Id, _productionData.CurrentValue);
                }
                catch (RpcException)
                {
                    result = false;
                }

                var text = $"{_productionData.Machine.Id} - {_productionData.Machine.DisplayName} - Produced = {_productionData.CurrentValue}, Sendt to api = {result} ";
                Console.WriteLine(text);
            }

        }
    }
}
