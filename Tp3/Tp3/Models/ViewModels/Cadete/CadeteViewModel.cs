using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class CadeteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        //public List<PedidoViewModel> pedidos { get; set; }

        public CadeteViewModel()
        {

        }
    }
}
