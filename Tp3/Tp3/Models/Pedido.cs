using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models
{
    public enum EstadoCadete
    {
        Aceptado,
        Entregado
    }
    public class Pedido
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private EstadoCadete estado;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoCadete Estado { get => estado; set => estado = value; }

        public Pedido(int nro, string obs, string nombre, string direccion, int telefono, int dni)
        {
            this.nro = nro;
            this.obs = obs;
            this.cliente = new Cliente();
            agregarDatosCliente(nombre, direccion, telefono, dni);
            estado = 0;
        }

        private void agregarDatosCliente(string nombre, string direccion, int telefono, int dni)
        {
            cliente.Id = dni;
            cliente.Nombre = nombre;
            cliente.Direccion = direccion;
            cliente.Telefono = telefono;
        }
    }
}
