using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NLog;

namespace SistemaCadeteria.Modelo
{
    public class DBTemporal
    {
        private readonly ILogger _logger;
        public Cadeteria cadeteria { get; set; }

        public DBTemporal(ILogger logger)
        {
            _logger = logger;
            cadeteria = new Cadeteria();
            cadeteria.cadetes = leerArchivoCadetes();
            //agregarPedidosGuardados();
        }

        public List<Cadete> leerArchivoCadetes()
        {
            List<Cadete> listaCadetes;
            string rutaArchivo = @"Cadetes.json";

            try
            {
                using (StreamReader leerJason = File.OpenText(rutaArchivo))
                {
                    var Json = leerJason.ReadToEnd();
                    listaCadetes = JsonSerializer.Deserialize<List<Cadete>>(Json);
                }
            }
            catch (Exception ex)
            {
                listaCadetes = new List<Cadete>();
                string mensaje = "Error: " + ex.Message;
                mensaje += "Stack trace: " + ex.StackTrace;
                _logger.Error(mensaje);
            }

            return listaCadetes;
        }

        public void guardarCadete(Cadete cadete)
        {
           cadeteria.cadetes.Add(cadete);

            guardarJson(cadeteria.cadetes);
        }

        public void borrarCadete(int id)
        {
            cadeteria.cadetes.RemoveAt(id);

            guardarJson(cadeteria.cadetes);
        }

        public void modificarInfoCadete(Cadete cad)
        {
            foreach (var item in cadeteria.cadetes)
            {
                if(item.Id == cad.Id)
                {
                    item.Nombre = cad.Nombre;
                    item.Telefono = cad.Telefono;
                    item.Direccion = cad.Direccion;
                    break;
                }
            }

            guardarJson(cadeteria.cadetes);
        }

        public void guardarJson(List<Cadete> listaCadetes)
        {
            try
            { 
                FileStream archiboCadetes = new FileStream("Cadetes.json", FileMode.Create);
                StreamWriter escribirCadete = new StreamWriter(archiboCadetes);

                string strJson = JsonSerializer.Serialize(listaCadetes);
                escribirCadete.WriteLine("{0}", strJson);

                escribirCadete.Close();
                escribirCadete.Dispose();
            }
            catch(Exception ex)
            {
                string mensaje = "Error: " + ex.Message;
                mensaje += "Stack Trace: " + ex.StackTrace;
                _logger.Error(mensaje);
            }
        }

        public void actualizarBD()
        {
            guardarJson(cadeteria.cadetes);
        }

        /*private void agregarPedidosGuardados()
        {
            foreach(var cadete in cadeteria.cadetes)
            {
                foreach(var pedido in cadete.ListaPedido)
                {
                    cadeteria.pedidos.Add(pedido);
                }
            }
        }*/
    }

    
}
