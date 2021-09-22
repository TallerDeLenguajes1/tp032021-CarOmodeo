using Microsoft.AspNetCore.Mvc;
using SistemaCadeteria.Modelo;
using System.Collections.Generic;
using System.Linq;

namespace Tp3.Controllers
{
    public class PedidoController : Controller
    {
        static int id = 0;
        private readonly DBTemporal _DB;

        public PedidoController(DBTemporal _DB)
        {
            this._DB = _DB;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AltaPedido()
        {
            return View();
        }

        public IActionResult VistaPedido(string obs, string nombre, string direccion, string tel, int dni)
        {
            Pedido nuevoPedido;
            if (nombre != null && direccion != null && tel != null && dni != 0 && obs != null)
            {
                id++;
                nuevoPedido = new Pedido(id, obs, nombre, direccion, tel, dni);
                _DB.cadeteria.pedidos.Add(nuevoPedido);
            }            

            return View(_DB.cadeteria);
        }

        public IActionResult MostrarPedido(int id)
        { 
            return View(_DB.cadeteria.cadetes[_DB.cadeteria.cadetes.IndexOf(_DB.cadeteria.cadetes.Where(a => a.Id == id).First())]);
        }

        public IActionResult AsignarCadete(int idPedido, int idCadete)
        {
            Pedido pedido = _DB.cadeteria.pedidos.Where(a => a.Nro == idPedido).First();
            quitarPedidoCadete(pedido);

            Cadete cadete = _DB.cadeteria.cadetes.Where(a => a.Id == idCadete).First();
            cadete.ListaPedido.Add(pedido);

            return View("VistaPedido", _DB.cadeteria);
        }

        public IActionResult EliminarPedido(int idPedido)
        {
            eliminarUnPedido(idPedido);
            return View("VistaPedido", _DB.cadeteria);
        }

        public void eliminarUnPedido(int id)
        {
            Pedido pedido =_DB.cadeteria.pedidos.Where(a => a.Nro == id).First();
            _DB.cadeteria.pedidos.Remove(pedido);

            quitarPedidoCadete(pedido);
        }

        public void quitarPedidoCadete(Pedido pedido)
        {
            _DB.cadeteria.cadetes.ForEach(cad => cad.ListaPedido.Remove(pedido));
        }
    }
}
