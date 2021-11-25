using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaCadeteria.Modelo;
using Tp3.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Tp3.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly RepositorioUsuario repoUsuario;

        public UsuarioController(ILogger<UsuarioController> logger, RepositorioUsuario repositorioUsuario)
        {
            _logger = logger;
            repoUsuario = repositorioUsuario;
        }

        public IActionResult Usuario()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            try
            {
                int id = repoUsuario.GetUsuarioID(username, password);
                if (id != 0)
                {
                    HttpContext.Session.SetInt32("idUsuario", id);
                    return RedirectToAction("Usuario", "Usuario");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
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

                return RedirectToAction("Index", "Home");
            }            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
