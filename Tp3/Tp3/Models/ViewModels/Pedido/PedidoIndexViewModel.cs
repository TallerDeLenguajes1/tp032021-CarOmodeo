using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class PedidoIndexViewModel
    {
        public List<PedidoViewModel> Pedidos { get; set; }
        public List<CadeteViewModel> Cadetes { get; set; }

        public PedidoIndexViewModel(List<PedidoViewModel> Pedidos, List<CadeteViewModel> Cadetes)
        {
            this.Pedidos = Pedidos;
            this.Cadetes = Cadetes;
        }

        public PedidoIndexViewModel()
        {

        }
    }
}
