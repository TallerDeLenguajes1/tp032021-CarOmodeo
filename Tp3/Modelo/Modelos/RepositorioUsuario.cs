using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace SistemaCadeteria.Modelo
{ 
    public class RepositorioUsuario
    {
        private readonly ILogger _logger;
        private readonly string connectionString;

        public RepositorioUsuario(ILogger logger, string connectionString)
        {
            _logger = logger;
            this.connectionString = connectionString;
        }

        public void CreateUsuario(Usuario usuario)
        {
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string instruccion = @"INSERT INTO Usuarios (usuario, password)
                                         VALUES (@nombre, @password);";

                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@password", usuario.Password);

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                string mensaje = "Error Message: " + ex.Message;
                mensaje += " Stack trace: " + ex.StackTrace;
                _logger.Error(mensaje);
            }
        }

        public int GetUsuarioID(string nombre, string password)
        {
            int usuarioID = 0;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string instruccion = @"SELECT usuarioID FROM Usuarios
                                     WHERE usuario = @nombre AND password = @password;";

                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@password", password);

                        conexion.Open();

                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            DataReader.Read();
                            usuarioID = Convert.ToInt32(DataReader["usuarioID"]);
                            conexion.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                string mensaje = "Error Message: " + ex.Message;
                mensaje += " Stack trace: " + ex.StackTrace;
                _logger.Error(mensaje);
            }

            return usuarioID;
        }

        public bool identidicadorValido(int id)
        {
            bool esValido = false;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string instruccion = @"SELECT usuarioID FROM Usuarios WHERE usuarioID = @id;";

                    using(SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conexion.Open();

                        using(SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            DataReader.Read();
                            int idUsuario = Convert.ToInt32(DataReader["usuarioID"]);
                            conexion.Close();

                            if(id == idUsuario)
                            {
                                esValido = true;
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                string mensaje = "Error Message: " + ex.Message;
                mensaje += " Stack trace: " + ex.StackTrace;
                _logger.Error(mensaje);
            }

            return esValido;
        }


    }
}
