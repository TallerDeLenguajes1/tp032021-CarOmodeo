using AutoMapper;
using SistemaCadeteria.Modelo;
using Tp3.Models.ViewModels;

namespace Tp3
{
    public class PerfilesDeMapeo : Profile
    {
        public PerfilesDeMapeo()
        {
            //Mapper de Cadetes
            CreateMap<Cadete, CadeteViewModel>().ReverseMap();
            CreateMap<Cadete, AltaCadeteViewModel>().ReverseMap();
            CreateMap<Cadete, EliminarCadeteViewModel>().ReverseMap();
            CreateMap<Cadete, ModificarCadeteViewModel>().ReverseMap();

            //Mapper de Pedido
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
            CreateMap<Pedido, AltaPedidoViewModel>().ReverseMap();
            CreateMap<Pedido, ModificarPedidoViewModel>().ReverseMap();

            //Mapper de Cliente
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();

            //Mapper de Usuario
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Usuario, AltaUsuarioViewModel>().ReverseMap();
            CreateMap<Usuario, ModificarUsuarioViewModel>().ReverseMap();
        }
    }
}
