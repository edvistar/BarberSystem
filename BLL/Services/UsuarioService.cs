using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public UsuarioService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public Task Actualizar(UsuarioAplicacion modeloDto)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioAplicacion> Agregar(UsuarioAplicacion modeloDto)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<UsuarioAplicacion>> ObtenerTodos()
        {
            try
            {

                var lista = await _unitWork.Usuario.ObtenerTodos(
                            orderby: e => e.OrderBy(e => e.Nombres));
                return _mapper.Map<IEnumerable<UsuarioAplicacion>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
