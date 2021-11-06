using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaCadeteria.Modelo;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Tp3.Controllers
{
    public class CadeteController : Controller
    {
        private readonly ILogger<CadeteController> _logger;
        private readonly RepositorioCadetes repoCadetes;

        public CadeteController(ILogger<CadeteController> logger, RepositorioCadetes repoCadetes)
        {
            _logger = logger;
            this.repoCadetes = repoCadetes;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AltaCadete()
        {
            return View();
        }

        public IActionResult VistaCadete(int id, string nombre, string direccion, string tel)
        {
            try
            {
                if (nombre != null && direccion != null && tel != null)
                {
                    if (id == 0)
                    {
                        //int id = _DB.cadeteria.cadetes.Count() + 1;
                        //id = _DB.cadeteria.cadetes.Last().Id + 1;
                        Cadete nuevoCadete = new Cadete(id, nombre, direccion, tel);
                        repoCadetes.insertCadete(nuevoCadete);
                    }
                    else
                    {
                        Cadete cadete = new Cadete(id, nombre, direccion, tel);
                        repoCadetes.updateCadete(cadete);
                    }
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
            
            return View(repoCadetes.getAllCadetes());
        }

        public IActionResult EliminarCadete(int id)
        {
            repoCadetes.deleteCadete(id);
            return View("VistaCadete", repoCadetes.getAllCadetes());
        }

        public IActionResult ModificarCadete(int id)
        {
            Cadete cadete = repoCadetes.selectCadete(id);
            return View(cadete);
        }
    }
}
