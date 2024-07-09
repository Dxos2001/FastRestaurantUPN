using FastRestaurant.Entidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace FastRestaurant.Dato
{
    public class dtoRestaurante
    {
        string cadenaBD = "";
        public dtoRestaurante(string cadena)
        {
            cadenaBD = cadena;
        }

        public async Task<DataCollection<Restaurante>> GetListRestaurante(paramGetListRestaurante obj)
        {
            var data = new DataCollection<Restaurante>();
            var lista = new List<Restaurante>();
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_listarRestaurante", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Restaurante usuario = new Restaurante();
                        try { usuario.id = dr.GetInt32("id"); } catch (Exception ex) { usuario.id = null; }
                        try { usuario.nombre = dr.GetString("nombre"); } catch (Exception ex) { usuario.nombre = null; }
                        try { usuario.descripcion = dr.GetString("descripcion"); } catch (Exception ex) { usuario.descripcion = null; }
                        try { usuario.direccion = dr.GetString("direccion"); } catch (Exception ex) { usuario.direccion = null; }
                        try { usuario.telefono = dr.GetString("telefono"); } catch (Exception ex) { usuario.telefono = null; }
                        try { usuario.imagen = dr.GetString("imagen"); } catch (Exception ex) { usuario.imagen = null; }
                        lista.Add(usuario);

                    }
                    data.Items = lista;
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
            return data;
        }

        public async Task<string> SaveRestaurante(Restaurante obj)
        {
            var data = new Restaurante();
            string mensaje = "";
            var registro = 0;
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_guardarRestaurante", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("p_descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("p_direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("p_telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("p_imagen", obj.imagen);
                    cmd.CommandType = CommandType.StoredProcedure;
                    registro = cmd.ExecuteNonQuery();
                    if (registro != 0)
                    {
                        mensaje = "Registro exitoso";
                    }
                    else
                    {
                        mensaje = "Error en el registro";
                    }
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return mensaje;
        }
    }
}
