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
        private readonly DBTemporal _DB;
        private readonly ILogger<CadeteController> _logger;

        public CadeteController(ILogger<CadeteController> logger, DBTemporal _DB)
        {
            _logger = logger;
            this._DB = _DB;
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
                        id = _DB.cadeteria.cadetes.Last().Id + 1;
                        Cadete nuevoCadete = new Cadete(id, nombre, direccion, tel);
                        _DB.guardarCadete(nuevoCadete);
                    }
                    else
                    {
                        Cadete cadete = new Cadete(id, nombre, direccion, tel);
                        _DB.modificarInfoCadete(cadete);
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
            
            return View(_DB.cadeteria.cadetes);
        }

        public IActionResult EliminarCadete(int id)
        {
            int i = 0;
            foreach (var item in _DB.cadeteria.cadetes)
            {
                if(item.Id == id)
                {
                    _DB.borrarCadete(i);
                    break;
                }
                i++;
            }
            return View("VistaCadete", _DB.cadeteria.cadetes);
        }

        public IActionResult ModificarCadete(int id)
        {
            Cadete cadete = _DB.cadeteria.cadetes.Where(a => a.Id == id).First();
            return View(cadete);
        }
    }
}
