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
            CreateMap<Cadete, EliminarCadeteViewModel>().ReverseMap();
            CreateMap<Cadete, ModificarCadeteViewModel>().ReverseMap();
            CreateMap<Cadete, VistaCadeteViewModel>().ReverseMap();

            //Mapper de Pedido
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
            CreateMap<Pedido, AltaPedidoViewModel>().ReverseMap();
            
            //Mapper de Cliente
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}
