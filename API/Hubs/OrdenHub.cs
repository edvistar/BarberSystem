using Microsoft.AspNetCore.SignalR;
using Models.Entities;

namespace API.Hubs
{
    public class OrdenHub : Hub
    {
        // Método para notificar a todos los clientes que una silla ha sido ocupada
        public async Task UpdateChairStatus(int chairId, string status, List<Service> services)
        {
            // Difundir la actualización a todos los clientes conectados
            await Clients.All.SendAsync("ReceiveChairUpdate", chairId, status, services);
        }
    }
}
