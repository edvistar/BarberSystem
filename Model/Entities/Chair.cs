﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Chair
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Numero { get; set; }
        public string Logo { get; set; }
        public bool Ocuped { get; set; }
    }
}
