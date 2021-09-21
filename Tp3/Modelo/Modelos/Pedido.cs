﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteria.Modelo
{
    public enum EstadoPedido
    {
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

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }

        public Pedido(int nro, string obs, string nombre, string direccion, string telefono, int dni)
        {
            this.nro = nro;
            this.obs = obs;
            this.cliente = new Cliente(nombre, direccion, telefono, dni);
            estado = 0;
        }

    }
}