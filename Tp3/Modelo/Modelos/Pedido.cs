using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteria.Modelo
{
    public enum EstadoPedido
    {
        Ingresado,
        Aceptado,
        EnCamino,
        Entregado,
        Cancelado
    }
    public class Pedido
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private EstadoPedido estado;
        private int idCadete;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }

        public Pedido(int nro, string obs, string nombre, string direccion, string telefono, int dni)
        {
            this.nro = nro;
            this.obs = obs;
            this.cliente = new Cliente(nombre, direccion, telefono, dni);
            estado = 0;
        }

        public Pedido()
        {

        }

    }
}
