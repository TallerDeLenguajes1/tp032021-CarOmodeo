using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaCadeteria.Modelo;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Tp3.Models.ViewModels;

namespace Tp3.Controllers
{
    public class CadeteController : Controller
    {
        private readonly ILogger<CadeteController> _logger;
        private readonly IRepositorioCadetes repoCadetes;
        private readonly RepositorioUsuario repoUsuario;
        private readonly IMapper mapper;

        public CadeteController(ILogger<CadeteController> logger, IRepositorioCadetes repoCadetes, RepositorioUsuario repositorioUsuario, IMapper mapper)
        {
            _logger = logger;
            this.repoCadetes = repoCadetes;
            repoUsuario = repositorioUsuario;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    return View("AltaCadete", new AltaCadeteViewModel());
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

        public IActionResult AltaCadete(AltaCadeteViewModel nuevoCadeteViewModel)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    Cadete nuevoCadete = mapper.Map<Cadete>(nuevoCadeteViewModel);
                    repoCadetes.insertCadete(nuevoCadete);

                    return RedirectToAction(nameof(VistaCadete));
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

        public IActionResult VistaCadete()
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    return View(mapper.Map<List<CadeteViewModel>>(repoCadetes.getAllCadetes()));
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

        public IActionResult EliminarCadete(int id)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    repoCadetes.deleteCadete(id);
                    return RedirectToAction(nameof(VistaCadete));
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
                    Cadete cadete = repoCadetes.selectCadete(id);

                    return View("ModificarCadete", mapper.Map<ModificarCadeteViewModel>(cadete));
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

        public IActionResult ModificarCadete(ModificarCadeteViewModel cadeteViewModel)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador) && ModelState.IsValid)
                {
                    repoCadetes.updateCadete(mapper.Map<Cadete>(cadeteViewModel));
                    return RedirectToAction(nameof(VistaCadete));
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
