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
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    ViewBag.rol = devolverRol();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }                     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            try
            {
                Usuario usuario = repoUsuario.GetUsuarioPorID(username, password);
                if (usuario.Id != 0)
                {
                    HttpContext.Session.SetInt32("idUsuario", usuario.Id);
                    HttpContext.Session.SetString("usuarioRol", usuario.Rol.ToString());
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.rol = devolverRol();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.rol = devolverRol();
                return View("AltaUsuario", new AltaUsuarioViewModel());
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaUsuario(AltaUsuarioViewModel nuevoUsuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario nuevoUsuario = mapper.Map<Usuario>(nuevoUsuarioViewModel);
                    repoUsuario.insertUsuario(nuevoUsuario);

                    ViewBag.rol = devolverRol();
                    return RedirectToAction("GuardarUsuario",nuevoUsuario);
                    
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
                    ViewBag.rol = devolverRol();
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
                    ViewBag.rol = devolverRol();
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

                    ViewBag.rol = devolverRol();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarUsuario(ModificarUsuarioViewModel usuarioViewModel)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    repoUsuario.updateUsuario(mapper.Map<Usuario>(usuarioViewModel));

                    ViewBag.rol = devolverRol();
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

        public IActionResult GuardarUsuario(Usuario nuevoUsuario)
        {
            try
            {
                ViewBag.rol = devolverRol();
                return View(mapper.Map<UsuarioViewModel>(nuevoUsuario));
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult CerrarSesion()
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    ViewBag.rol = devolverRol();
                    Usuario usuario = repoUsuario.selectUsuario(HttpContext.Session.GetInt32("idUsuario").Value);
                    return View(mapper.Map<UsuarioViewModel>(usuario));
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

        public IActionResult borrarVariablesSesion()
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    HttpContext.Session.Clear();
                }                                          
            }
            catch (Exception ex)
            {
                guardarMensajeError(ex);                
            }

            return RedirectToAction("Index", "Home");
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

        private string devolverRol()
        {
            return HttpContext.Session.GetString("usuarioRol");
        }
    }
}
