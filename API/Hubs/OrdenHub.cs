using Microsoft.AspNetCore.SignalR;
using Models.Entities;

namespace API.Hubs
{
    public class OrdenHub : Hub
    {
        // Método para notificar a todos los clientes que una silla ha sido ocupada
        public async Task UpdateChairStatus(int chairId, bool ocuped, List<Service> services)
        {
            // Difundir la actualización a todos los clientes conectados
            await Clients.All.SendAsync("ReceiveChairUpdate", chairId, ocuped, services);
        }

        // Método para notificar a todos los clientes que se ha agregado un servicio a una silla
        public async Task NotifyServiceAdded(int chairId, List<Service> services)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveServiceUpdate", chairId, services);
            }
            catch (Exception ex)
            {
                // Log el error o manejarlo como sea necesario
                Console.WriteLine($"Error enviando notificación: {ex.Message}");
            }
        }
    }
}
