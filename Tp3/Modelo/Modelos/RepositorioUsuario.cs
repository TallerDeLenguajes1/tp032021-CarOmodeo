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

        public void insertUsuario(Usuario usuario)
        {
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string instruccion = @"INSERT INTO Usuarios (usuario, password, usuarioActivo)
                                         VALUES (@nombre, @password, 1);";

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
                mensajeError(ex);
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
                mensajeError(ex);
            }

            return usuarioID;
        }

        public bool identidicadorValido(int id)
        {
            bool esValido = false;

            try
            {
                Usuario usuario = selectUsuario(id);
                
                if(usuario.Id == id)
                {
                    esValido = true;
                }
            }
            catch(Exception ex)
            {
                mensajeError(ex);
            }            

            return esValido;
        }

        public Usuario selectUsuario(int id)
        {
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string instruccion = @"SELECT usuarioID, usuario, password FROM Usuarios WHERE usuarioID = @id;";

                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conexion.Open();

                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            DataReader.Read();

                            Usuario usuario = new Usuario()
                            {
                                Id = Convert.ToInt32(DataReader["usuarioID"]),
                                Nombre = DataReader["usuario"].ToString(),
                                Password = DataReader["password"].ToString(),                                
                            };

                            conexion.Close();
                            return usuario;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
                return new Usuario();
            }
        }

        public List<Usuario> getAllUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string instruccion = @"SELECT usuarioID, usuario, password FROM Usuarios 
                                        WHERE usuarioActivo = 1";

                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        var DataReader = command.ExecuteReader();

                        while (DataReader.Read())
                        {
                            Usuario usuario = new Usuario() 
                            {
                                Id = Convert.ToInt32(DataReader["usuarioID"]),
                                Nombre = DataReader["usuario"].ToString(),
                                Password = DataReader["password"].ToString()
                            };
                            
                            listaUsuarios.Add(usuario);
                        }
                        DataReader.Close();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }

            return listaUsuarios;
        }

        public void deleteUsuario(int id)
        {
            try
            {
                string instruccion = @"UPDATE Usuarios 
                                    SET usuarioActivo = 0
                                    WHERE usuarioID = @id";

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
                mensajeError(ex);
            }
        }

        public void updateUsuario(Usuario modificarUsuario)
        {
            try
            {
                string instruccion = @"UPDATE Usuarios 
                                    SET usuario = @nombre, password = @pass
                                    WHERE usuarioID = @usuarioID";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@usuarioID", modificarUsuario.Id);
                        command.Parameters.AddWithValue("@nombre", modificarUsuario.Nombre);
                        command.Parameters.AddWithValue("@pass", modificarUsuario.Password);
                        

                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void mensajeError(Exception ex)
        {
            string mensaje = "Error Message: " + ex.Message;
            mensaje += " Stack trace: " + ex.StackTrace;
            _logger.Error(mensaje);
        }
    }
}
