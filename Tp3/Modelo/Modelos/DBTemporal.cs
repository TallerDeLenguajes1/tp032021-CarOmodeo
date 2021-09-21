using System;
using System.Collections.Generic;
using System.IO;
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

            FileStream archiboCadetes = new FileStream("Cadetes.json", FileMode.Create);
            StreamWriter escribirCadete = new StreamWriter(archiboCadetes);

            string strJson = JsonSerializer.Serialize(listaCadetes);
            escribirCadete.WriteLine("{0}", strJson);

            escribirCadete.Close();

            return listaCadetes;
        }

    }
}
