using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCadeteria.Modelo
{
    public class Usuario
    {
        private string nombre;
        private string password;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Password { get => password; set => password = value; }

        public Usuario(string nombre, string password)
        {
            Nombre = nombre;
            Password = password;
        }
    }
}
