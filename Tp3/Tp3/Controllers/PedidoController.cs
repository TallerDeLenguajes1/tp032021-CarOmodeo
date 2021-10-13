using Microsoft.AspNetCore.Mvc;
using SistemaCadeteria.Modelo;
using System.Collections.Generic;
using System.Linq;
using NLog.Web;
using Microsoft.Extensions.Logging;
using System;

namespace Tp3.Controllers
{
    public class PedidoController : Controller
    {
        private readonly DBTemporal _DB;
        private readonly ILogger _logger;
        static int id;

        public PedidoController(ILogger<PedidoController> logger, DBTemporal _DB)
        {
            _logger = logger;
            this._DB = _DB;
            id = _DB.cadeteria.pedidos.Count();
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
            try
            {
                Pedido nuevoPedido;
                if (nombre != null && direccion != null && tel != null && dni != 0 && obs != null)
                {
                    id++;
                    nuevoPedido = new Pedido(id, obs, nombre, direccion, tel, dni);
                    _DB.cadeteria.pedidos.Add(nuevoPedido);
                }
            }    
            catch(Exception ex)
            {
                string mensaje = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + ex.InnerException.Message;
                }
                mensaje += "Stack trace: " + ex.StackTrace;
                _logger.LogError(mensaje);
            }

            return View(_DB.cadeteria);
        }

        public IActionResult MostrarPedido(int id)
        { 
            return View(_DB.cadeteria.cadetes[_DB.cadeteria.cadetes.IndexOf(_DB.cadeteria.cadetes.Where(a => a.Id == id).First())]);
        }

        public IActionResult AsignarCadete(int idPedido, int idCadete)
        {            
            try
            {
                Pedido pedido = _DB.cadeteria.pedidos.Where(a => a.Nro == idPedido).First();
                quitarPedidoCadete(pedido);

                Cadete cadete = _DB.cadeteria.cadetes.Where(a => a.Id == idCadete).First();
                cadete.ListaPedido.Add(pedido);
                pedido.Estado = EstadoPedido.Aceptado;
                _DB.actualizarBD();
            }
            catch(Exception ex)
            {
                string mensaje = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + ex.InnerException.Message;
                }
                mensaje += "Stack trace: " + ex.StackTrace;
                _logger.LogError(mensaje);
            }

            return View("VistaPedido", _DB.cadeteria);
        }

        public IActionResult EliminarPedido(int idPedido)
        {
            eliminarUnPedido(idPedido);
            _DB.actualizarBD();
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
