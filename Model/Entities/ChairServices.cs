using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class ChairServices
    {
        public int Id { get; set; }
        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
