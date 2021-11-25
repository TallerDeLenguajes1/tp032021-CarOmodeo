using System.Collections.Generic;

namespace SistemaCadeteria.Modelo
{
    public interface IRepositorioPedidos
    {
        void asignarCadeteAPedido(int idPedido, int idCadete);
        void deletePedido(int id);
        List<Pedido> getAllPedidos();
        void insertPedido(Pedido nuevo);
        Pedido selectPedido(int id);
        void updatePedido(Pedido modificarPedido);
    }
}