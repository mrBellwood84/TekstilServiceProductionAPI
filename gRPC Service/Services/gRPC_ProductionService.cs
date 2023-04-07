using Application.DataProvider;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Models.Production;
using SignalR_Service.Clients;
using SignalR_Service.Services;

namespace gRPC_Service.Services
{
    public class gRPC_ProductionService : Production.ProductionBase
    {
        private readonly ILogger<gRPC_ProductionService> _logger;
        private readonly IHubContext<SignalR_ProductionHub, IProductionClient> _hubContext;
        private readonly IDataService _data;

        public gRPC_ProductionService(ILogger<gRPC_ProductionService> logger, IHubContext<SignalR_ProductionHub, IProductionClient> hubContext, IDataService data)
        {
            _logger = logger;
            _hubContext = hubContext;
            _data = data;
        }

        public override Task<ProduceResponse> Produce(ProduceRequest request, ServerCallContext context)
        {
            try
            {
                var signalR_response = new ProductionUpdateDTO { MachineId = new Guid(request.MachineId), Value = request.Value };
                _hubContext.Clients.All.UpdateProductionValue(signalR_response);
                _data.UpdateProductionData(signalR_response);
                return Task.FromResult(new ProduceResponse { Result = true });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Task.FromResult(new ProduceResponse { Result = false });
            }

        }
    }
}
