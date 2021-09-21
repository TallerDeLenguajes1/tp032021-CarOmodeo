using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteria.Modelo
{
    public class Cadeteria
    {
        private string nombre;
        public List<Cadete> cadetes { get; set; }
        public List<Pedido> pedidos { get; set; }

        public string Nombre { get => nombre; set => nombre = value; }

        public Cadeteria()
        {
            cadetes = new List<Cadete>();
            pedidos = new List<Pedido>();
        }       
        
    }
}
