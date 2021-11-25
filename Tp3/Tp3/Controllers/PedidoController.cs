using Microsoft.AspNetCore.Mvc;
using SistemaCadeteria.Modelo;
using System.Collections.Generic;
using System.Linq;
using NLog.Web;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Tp3.Models;
using AutoMapper;
using Tp3.Models.ViewModels;

namespace Tp3.Controllers
{
    public class PedidoController : Controller
    {
        //private readonly DBTemporal _DB;
        private readonly IRepositorioPedidos repoPedidos;
        private readonly RepositorioUsuario repoUsuario;
        private readonly ILogger _logger;
        private readonly IMapper mapper;
        private readonly IRepositorioCadetes reposCadetes;

        public PedidoController(ILogger<PedidoController> logger, IRepositorioPedidos repositorioPedidos, RepositorioUsuario repositorioUsuario, IMapper mapper, IRepositorioCadetes repositorioCadetes)
        {
            _logger = logger;
            //this._DB = _DB;
            //id = _DB.cadeteria.pedidos.Count();
            repoPedidos = repositorioPedidos;
            repoUsuario = repositorioUsuario;
            reposCadetes = repositorioCadetes;
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
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    return View("AltaPedido", new Pedido());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }            
        }

        public IActionResult AltaPedido(Pedido nuevoPedido)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    repoPedidos.insertPedido(nuevoPedido);
                    return RedirectToAction(nameof(VistaPedido));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult VistaPedido()
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    var pedidos = mapper.Map<List<PedidoViewModel>>(repoPedidos.getAllPedidos());
                    var cadetes = mapper.Map<List<CadeteViewModel>>(reposCadetes.getAllCadetes());

                    PedidoIndexViewModel pedidosYCadetes = new PedidoIndexViewModel(pedidos,cadetes);
                    return View(pedidosYCadetes);                    
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }    
            catch(Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Edit(int idPedido)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {                    
                    return View("MostrarPedido", repoPedidos.selectPedido(idPedido));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult MostrarPedido(Pedido pedido)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    repoPedidos.updatePedido(pedido);
                    return RedirectToAction(nameof(VistaPedido));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AsignarCadete(int idPedido, int idCadete)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    repoPedidos.asignarCadeteAPedido(idPedido, idCadete);
                    return RedirectToAction(nameof(VistaPedido));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }           
        }

        public IActionResult EliminarPedido(int idPedido)
        {
            try
            {
                int identidicador = HttpContext.Session.GetInt32("idUsuario").Value;
                if (repoUsuario.identidicadorValido(identidicador))
                {
                    repoPedidos.deletePedido(idPedido);
                    return RedirectToAction(nameof(VistaPedido));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                guardarmensajeError(ex);
                return RedirectToAction("Index", "Home");
            }            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void guardarmensajeError(Exception ex)
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
