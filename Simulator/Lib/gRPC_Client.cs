using Grpc.Net.Client;

namespace Simulator.Lib
{
    public static class gRPC_Client
    {
        public static async Task<bool> Send(Guid machineId, int value)
        {
            var request = new ProduceRequest
            {
                MachineId = machineId.ToString(),
                Value = value
            };

            var channel = GrpcChannel.ForAddress("http://localhost:7030");
            var client = new Production.ProductionClient(channel);

            var reply = await client.ProduceAsync(request);
            return reply.Result;
        }
    }
}
