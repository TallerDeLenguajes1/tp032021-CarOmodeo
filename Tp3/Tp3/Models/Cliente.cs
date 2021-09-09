using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models
{
    public class Cliente
    {
        int id;
        string nombre;
        string direccion;
        int telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
    }
}
