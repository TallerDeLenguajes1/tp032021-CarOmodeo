using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SistemaCadeteria.Modelo
{
    public class DBTemporal
    {
        public Cadeteria cadeteria { get; set; }

        public DBTemporal()
        {
            cadeteria = new Cadeteria();
            cadeteria.cadetes = leerArchivoCadetes();
        }

        public static List<Cadete> leerArchivoCadetes()
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
            catch (FileNotFoundException)
            {
                listaCadetes = new List<Cadete>();
            }

            return listaCadetes;
        }

        public static List<Cadete> guardarCadete(Cadete cadete)
        {
            List<Cadete> listaCadetes = leerArchivoCadetes();

            listaCadetes.Add(cadete);

            guardarJson(listaCadetes);

            return listaCadetes;
        }

        public static List<Cadete> borrarCadete(int id)
        {
            List<Cadete> listaCadetes = leerArchivoCadetes();

            listaCadetes.RemoveAt(id);

            guardarJson(listaCadetes);

            return listaCadetes;
        }

        public static List<Cadete> modificarInfoCadete(Cadete cad)
        {
            List<Cadete> listaCadetes = leerArchivoCadetes();

            foreach (var item in listaCadetes)
            {
                if(item.Id == cad.Id)
                {
                    item.Nombre = cad.Nombre;
                    item.Telefono = cad.Telefono;
                    item.Direccion = cad.Direccion;
                    break;
                }
            }

            guardarJson(listaCadetes);

            return listaCadetes;
        }

        public static void guardarJson(List<Cadete> listaCadetes)
        {
            FileStream archiboCadetes = new FileStream("Cadetes.json", FileMode.Create);
            StreamWriter escribirCadete = new StreamWriter(archiboCadetes);

            string strJson = JsonSerializer.Serialize(listaCadetes);
            escribirCadete.WriteLine("{0}", strJson);

            escribirCadete.Close();
        }
    }

    
}
