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
using Tp3.Models.ViewModels;
using AutoMapper;

namespace Tp3.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly RepositorioUsuario repoUsuario;
        private readonly IMapper mapper;

        public UsuarioController(ILogger<UsuarioController> logger, RepositorioUsuario repositorioUsuario, IMapper mapper)
        {
            _logger = logger;
            repoUsuario = repositorioUsuario;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
            if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        public IActionResult Login(string username, string password)
        {
            try
            {
                int id = repoUsuario.GetUsuarioID(username, password);
                if (id != 0)
                {
                    HttpContext.Session.SetInt32("idUsuario", id);
                    return RedirectToAction("Index", "Usuario");
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

        public IActionResult Create()
        {
            try
            {
                return View("AltaUsuario", new AltaUsuarioViewModel());
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AltaUsuario(AltaUsuarioViewModel nuevoUsuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario nuevoUsuario = mapper.Map<Usuario>(nuevoUsuarioViewModel);
                    repoUsuario.insertUsuario(nuevoUsuario);

                    int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                    if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                    {
                        return RedirectToAction(nameof(VistaUsuarios));
                    }                                   
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult VistaUsuarios()
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    return View(mapper.Map<List<UsuarioViewModel>>(repoUsuario.getAllUsuarios()));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    repoUsuario.deleteUsuario(id);
                    return RedirectToAction(nameof(VistaUsuarios));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    Usuario usuario = repoUsuario.selectUsuario(id);

                    return View("ModificarUsuario", mapper.Map<ModificarUsuarioViewModel>(usuario));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ModificarUsuario(ModificarUsuarioViewModel usuarioViewModel)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    repoUsuario.updateUsuario(mapper.Map<Usuario>(usuarioViewModel));
                    return RedirectToAction(nameof(VistaUsuarios));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }

        }

        private void guardarMensajeError(Exception ex)
        {
            string mensaje = "Error: " + ex.Message;
            if (ex.InnerException != null)
            {
                mensaje = mensaje + " Inner exception: " + ex.InnerException.Message;
            }
            mensaje += "Stack trace: " + ex.StackTrace;
            _logger.LogError(mensaje);
        }


    }
}
