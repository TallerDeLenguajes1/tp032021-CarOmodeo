using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaCadeteria.Modelo;

namespace Tp3.Controllers
{
    public class CadeteController : Controller
    {
        private readonly DBTemporal _DB;

        public CadeteController(DBTemporal _DB)
        {
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

        public IActionResult VistaCadete(string nombre, string direccion, string tel)
        {
            if(nombre != null && direccion != null && tel != null)
            {
                int id = _DB.cadeteria.cadetes.Count() + 1;
                Cadete nuevoCadete = new Cadete(id, nombre, direccion, tel);
                _DB.cadeteria.cadetes = DBTemporal.guardarCadete(nuevoCadete);
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
                    _DB.cadeteria.cadetes = DBTemporal.borrarCadete(i);
                }
                i++;
            }
            return View("VistaCadete", _DB.cadeteria.cadetes);
        }
    }
}
