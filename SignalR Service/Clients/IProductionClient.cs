using Models.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Service.Clients
{
    public interface IProductionClient
    {
        Task UpdateProductionValue(ProductionUpdateDTO dto);
    }
}
