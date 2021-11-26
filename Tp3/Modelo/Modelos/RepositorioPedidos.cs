using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace SistemaCadeteria.Modelo
{
    public class RepositorioPedidos : IRepositorioPedidos
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioPedidos(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Pedido> getAllPedidos()
        {
            List<Pedido> listaPedidos = new List<Pedido>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string instruccion = @"SELECT pedidoID, pedidoObservacion, pedidoEstado, clienteNombre, clienteDireccion, clienteTelefono, clienteDNI, cadeteID FROM Pedidos 
                                    INNER JOIN Clientes using(clienteID)
                                    WHERE pedidoActivo = 1";

                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        var DataReader = command.ExecuteReader();

                        while (DataReader.Read())
                        {
                            Pedido pedido = new Pedido(Convert.ToInt32(DataReader["pedidoID"]), DataReader["pedidoObservacion"].ToString(), DataReader["clienteNombre"].ToString(), DataReader["clienteDireccion"].ToString(), DataReader["clienteTelefono"].ToString(), Convert.ToInt32(DataReader["clienteDNI"]));
                            pedido.Estado = (EstadoPedido)Convert.ToInt32(DataReader["pedidoEstado"]);
                            if (!DataReader.IsDBNull(DataReader.GetOrdinal("cadeteID")))
                            {
                                pedido.IdCadete = Convert.ToInt32(DataReader["cadeteID"]);
                            }
                            else
                            {
                                pedido.IdCadete = 0;
                            }

                            listaPedidos.Add(pedido);
                        }
                        DataReader.Close();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de Error: " + ex.Message;
            }

            return listaPedidos;
        }

        public void updateCliente(Cliente modificarCliente)
        {
            try
            {
                string instruccion = @"UPDATE Clientes
                                        SET clienteNombre = @nombre, clienteDireccion = @direccion,
                                        clienteTelefono = @telefono
                                        WHERE clienteDNI = @clienteID";

                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@clienteID", modificarCliente.Id);
                        command.Parameters.AddWithValue("@nombre", modificarCliente.Nombre);
                        command.Parameters.AddWithValue("@telefono", modificarCliente.Telefono);
                        command.Parameters.AddWithValue("@direccion", modificarCliente.Direccion);

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }
        }

        public void updatePedido(Pedido modificarPedido)
        {
            try
            {
                updateCliente(modificarPedido.Cliente);

                string instruccion = @"UPDATE Pedidos
                                        SET pedidoObservacion = @observacion
                                        WHERE pedidoID = @pedidoID";

                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@pedidoID", modificarPedido.Nro);
                        command.Parameters.AddWithValue("@observacion", modificarPedido.Obs);
                        
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }
        }

        public void deletePedido(int id)
        {
            try
            {
                string instruccion = @"UPDATE Pedidos 
                                    SET pedidoActivo = 0
                                    WHERE pedidoID = @id";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }

        }

        private void insertCliente(Cliente nuevo)
        {
            try
            {
                string instruccion = @"INSERT INTO                                   
                                       Clientes (clienteNombre, clienteDireccion, clienteTelefono, clienteDNI)
                                       VALUES (@nombre, @direccion, @telefono, @dni)";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                        command.Parameters.AddWithValue("@direccion", nuevo.Direccion);
                        command.Parameters.AddWithValue("@telefono", nuevo.Telefono);
                        command.Parameters.AddWithValue("@dni", nuevo.Id);

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }
        }

        public void insertPedido(Pedido nuevo)
        {
            try
            {
                insertCliente(nuevo.Cliente);

                string instruccion = @"INSERT INTO                                     
                                       Pedidos (pedidoObservacion, pedidoEstado, pedidoActivo, clienteID)
                                       VALUES (@obs, 0, 1, @clienteID)";

                int id = selectIDCliente(nuevo.Cliente);

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@obs", nuevo.Obs);
                        command.Parameters.AddWithValue("@clienteID", id);

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }
        }

        public Pedido selectPedido(int id)
        {
            Pedido pedido;

            try
            {
                string instruccion = @"SELECT pedidoID, pedidoObservacion, clienteDNI, clienteNombre, clienteDireccion,
                                         clienteTelefono, pedidoEstado FROM Pedidos 
                                         INNER JOIN Clientes using(clienteID)
                                         WHERE pedidoID = @id";

                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conexion.Open();

                        var DataReader = command.ExecuteReader();
                        DataReader.Read();

                        pedido = new Pedido(Convert.ToInt32(DataReader["pedidoId"]), DataReader["pedidoObservacion"].ToString(), DataReader["clienteNombre"].ToString(), DataReader["clienteDireccion"].ToString(), DataReader["clienteTelefono"].ToString(), Convert.ToInt32(DataReader["clienteDNI"]));
                        pedido.Estado = (EstadoPedido)Convert.ToInt32(DataReader["pedidoEstado"]);

                        DataReader.Close();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de Error: " + ex.Message;
                pedido = new Pedido();
            }

            return pedido;
        }

        private int selectIDCliente(Cliente cliente)
        {
            string intruccion = "SELECT clienteID FROM Clientes WHERE clienteDNI = @dni";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(intruccion, conexion))
                {
                    command.Parameters.AddWithValue("@dni", cliente.Id);

                    conexion.Open();

                    var DataReader = command.ExecuteReader();
                    DataReader.Read();

                    int id = Convert.ToInt32(DataReader["clienteID"]);

                    DataReader.Close();
                    conexion.Close();

                    return id;
                }
            }
        }

        public void asignarCadeteAPedido(int idPedido, int idCadete)
        {
            try
            {

                string instruccion = @"UPDATE Pedidos SET cadeteID = @cadeteID, pedidoEstado = @estado
                                     WHERE pedidoID = @pedidoID";

                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteID", idCadete);
                        command.Parameters.AddWithValue("@estado", EstadoPedido.Aceptado);
                        command.Parameters.AddWithValue("@pedidoID", idPedido);

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }
        }
    }
}
