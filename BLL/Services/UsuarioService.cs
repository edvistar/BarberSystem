using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
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
        private readonly UserManager<UsuarioUpdateDto> _userManager;

        public UsuarioService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public async Task Actualizar(UsuarioUpdateDto modeloDto)
        {
            try
            {
                var usuarioDb = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == modeloDto.Id);
                if (usuarioDb == null)
                    throw new TaskCanceledException("El servicio no Existe");
                //usuarioDb.Nombres = modeloDto.Nombres;
                //usuarioDb.Apellidos = modeloDto.Apellidos;
                //usuarioDb.UserName = modeloDto.UserName;
                //usuarioDb.Rol = modeloDto.Rol;
                //usuarioDb.Address = modeloDto.Address;
                //usuarioDb.Documento = modeloDto.Documento;
                //usuarioDb.PhoneNumber = modeloDto.PhoneNumber;
               /// _unitWork.Usuario.Actualizar(usuarioDb);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<UsuarioAplicacion> Agregar(UsuarioAplicacion modeloDto)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<UsuarioAplicacion>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public async Task Remover(int id)
        {
            var usuarioDb = await _unitWork.Usuario.ObtenerPrimero(e => e.Id == id);
            if (usuarioDb == null) throw new TaskCanceledException("La Silla no Existe");
            _unitWork.Usuario.Remover(usuarioDb);
            await _unitWork.Guardar();
        }
    }
}
