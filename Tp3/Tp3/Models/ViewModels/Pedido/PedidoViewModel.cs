using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public enum EstadoPedido
    {
        Ingresado,
        Aceptado,
        EnCamino,
        Entregado,
        Cancelado
    }
    public class PedidoViewModel
    {
        public int Nro { get; set; }
        public string Obs { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public EstadoPedido Estado { get; set; }

        public int IdCadete { get; set; }

        public PedidoViewModel()
        {

        }
    }
}
