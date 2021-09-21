using Microsoft.AspNetCore.Mvc;
using SistemaCadeteria.Modelo;
using System.Collections.Generic;

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

            return View(_DB.cadeteria.pedidos);
        }

        public IActionResult MostrarPedido(Cadete cadete)
        {
            return View(cadete.ListaPedido);
        }
    }
}
