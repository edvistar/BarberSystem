using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ChairEstadoDto
    {
        public int Id { get; set; }
        public bool Ocuped { get; set; } = false;
    }
}
