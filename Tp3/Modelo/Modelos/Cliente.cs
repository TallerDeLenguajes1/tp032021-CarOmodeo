using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteria.Modelo
{
    public class Cliente
    {
        int id;
        string nombre;
        string direccion;
       string telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Cliente(string nombre, string direccion, string telefono, int dni)
        {
            this.id = dni;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }

        public Cliente()
        {

        }
    }
}
