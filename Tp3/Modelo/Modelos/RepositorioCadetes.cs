using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using SistemaCadeteria.Modelo;

namespace SistemaCadeteria.Modelo
{
    public class RepositorioCadetes : IRepositorioCadetes
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioCadetes(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Cadete> getAllCadetes()
        {
            List<Cadete> listaCadetes = new List<Cadete>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string instrucionSQL = "SELECT * FROM Cadetes WHERE cadeteActivo = 1;";
                    using (SQLiteCommand command = new SQLiteCommand(instrucionSQL, conexion))
                    {
                        var DataReader = command.ExecuteReader();

                        while (DataReader.Read())
                        {
                            Cadete cadete = new Cadete()
                            {
                                Id = Convert.ToInt32(DataReader["cadeteID"]),
                                Nombre = DataReader["cadeteNombre"].ToString(),
                                Telefono = DataReader["cadeteTelefono"].ToString(),
                                Direccion = DataReader["cadeteDireccion"].ToString(),
                            };

                            listaCadetes.Add(cadete);
                        }

                        DataReader.Close();
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }

            return listaCadetes;
        }

        public void updateCadete(Cadete modificarCadete)
        {
            try
            {
                string instruccion = @"UPDATE Cadetes 
                                    SET cadeteNombre = @nombre, cadeteTelefono = @telefono, cadeteDireccion = @direccion
                                    WHERE cadeteID = @cadeteID";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteID", modificarCadete.Id);
                        command.Parameters.AddWithValue("@nombre", modificarCadete.Nombre);
                        command.Parameters.AddWithValue("@telefono", modificarCadete.Telefono);
                        command.Parameters.AddWithValue("@direccion", modificarCadete.Direccion);

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

        public void deleteCadete(int id)
        {
            try
            {
                string instruccion = @"UPDATE Cadetes 
                                    SET cadeteActivo = 0
                                    WHERE cadeteID = @id";

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

        public void insertCadete(Cadete nuevo)
        {
            try
            {
                string instruccion = @"INSERT INTO 
                                       Cadetes (cadeteNombre, cadeteTelefono, cadeteDireccion, cadeteActivo)
                                       VALUES (@nombre, @telefono, @direccion, 1)";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                        command.Parameters.AddWithValue("@telefono", nuevo.Telefono);
                        command.Parameters.AddWithValue("@direccion", nuevo.Direccion);

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

        public Cadete selectCadete(int id)
        {
            string intruccion = "SELECT * FROM Cadetes WHERE cadeteID = @id";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(intruccion, conexion))
                {
                    command.Parameters.AddWithValue("@id", id);

                    conexion.Open();

                    var DataReader = command.ExecuteReader();
                    DataReader.Read();

                    Cadete cadete = new Cadete()
                    {
                        Id = Convert.ToInt32(DataReader["cadeteID"]),
                        Nombre = DataReader["cadeteNombre"].ToString(),
                        Telefono = DataReader["cadeteTelefono"].ToString(),
                        Direccion = DataReader["cadeteDireccion"].ToString(),
                    };

                    DataReader.Close();
                    conexion.Close();

                    return cadete;
                }
            }
        }
    }
}
