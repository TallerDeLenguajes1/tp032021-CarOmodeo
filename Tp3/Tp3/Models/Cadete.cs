using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private int telefono;
        private List<Pedido> listaPedido;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public List<Pedido> ListaPedido { get => listaPedido; }

        public Cadete(int id, string nombre, string direccion, int telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            listaPedido = new List<Pedido>();
        }

        public void agregarPedido(Pedido nuevoPedido)
        {
            listaPedido.Add(nuevoPedido);
        }

        public void quitarPedido(Pedido pedido)
        {
            listaPedido.Remove(pedido);
        }
    }


}
