using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Orden
    {
        public int Id { get; set; } // Número de la orden
        public int Numero { get; set; }
        public List<string> Servicios { get; set; } // Lista de servicios
        public string NombreCliente { get; set; } // Nombre del cliente
        public string UsuarioAtiende { get; set; } // Usuario que atiende
    }
}
