using Microsoft.AspNetCore.SignalR;
using Models.Entities;

namespace API.Hubs
{
    public class OrdenHub : Hub
    {
        // Método para notificar a todos los clientes que una silla ha sido ocupada
        public async Task UpdateChairStatus(int chairId, bool ocuped, List<Service> services)
        {
            try
            {
               
                var ocupedNumber = ocuped ? 1 : 0;
                Console.WriteLine($"Enviando actualización de ocupacion: Silla {chairId}, Ocupada: {ocupedNumber}, Servicios: {services.Count}");
                // Difundir la actualización a todos los clientes conectados
                await Clients.All.SendAsync("ReceiveChairUpdate", chairId, ocupedNumber, services);
            }
            catch (Exception ex)
            {

                // Log el error o manejarlo como sea necesario
                Console.WriteLine($"Error enviando notificación: {ex.Message}");
            }
        }

        // Método para notificar a todos los clientes que se ha agregado un servicio a una silla
        public async Task NotifyServiceAddedRemoved(int chairId, int serviceId, List<Service> services)
        {
            try
            {
                Console.WriteLine($"Enviando actualización de servicios: Silla {chairId}, Servicio ID: {serviceId}, Cantidad de Servicios: {services.Count}");
                await Clients.All.SendAsync("ReceiveServiceUpdate", chairId, serviceId, services);
            }
            catch (Exception ex)
            {
                // Log el error o manejarlo como sea necesario
                Console.WriteLine($"Error enviando notificación: {ex.Message}");
            }
        }



    }
}
