using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ChairservicesDto
    {
        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
