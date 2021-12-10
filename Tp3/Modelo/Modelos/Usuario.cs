using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCadeteria.Modelo
{
    public enum roles
    {
        administrador,
        operador
    }

    public class Usuario
    {
        private int id;
        private string nombre;
        private string password;
        private roles rol;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Password { get => password; set => password = value; }
        public int Id { get => id; set => id = value; }
        public roles Rol { get => rol; set => rol = value; }

        public Usuario(string nombre, string password)
        {
            Nombre = nombre;
            Password = password;
        }

        public Usuario()
        {
            
        }
    }
}
