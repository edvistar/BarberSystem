﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository
{
    public interface IUnitWork : IDisposable
    {
        IChairRepository  Chair {  get; }
        IServiceRepository Service { get; }
        IOrdenRepository Orden { get; }

        Task Guardar();
    }
}