using Microsoft.AspNetCore.SignalR;
using Models.Production;
using SignalR_Service.Clients;

namespace SignalR_Service.Services
{
    public class SignalR_ProductionHub : Hub<IProductionClient>
    {
        public async Task SendUpdatedProductionValue(ProductionUpdateDTO data)
        {
            await Clients.All.UpdateProductionValue(data);
        }
    }
}
