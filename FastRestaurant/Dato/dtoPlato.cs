using FastRestaurant.Entidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace FastRestaurant.Dato
{
    public class dtoPlato
    {
        string cadenaBD = "";
        public dtoPlato(string cadena)
        {
            cadenaBD = cadena;
        }

        public async Task<DataCollection<Plato>> GetListPlato(paramGetListPlato obj)
        {
            var data = new DataCollection<Plato>();
            var lista = new List<Plato>();
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_listarPlato", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Plato usuario = new Plato();
                        try { usuario.id = dr.GetInt32("id"); } catch (Exception ex) { usuario.id = null; }
                        try { usuario.nombre = dr.GetString("nombre"); } catch (Exception ex) { usuario.nombre = null; }
                        try { usuario.descripcion = dr.GetString("descripcion"); } catch (Exception ex) { usuario.descripcion = null; }
                        try { usuario.idRestaurante = dr.GetInt32("idRestaurante"); } catch (Exception ex) { usuario.idRestaurante = null; }
                        try { usuario.precio = dr.GetDecimal("precio"); } catch (Exception ex) { usuario.precio = 0; }
                        lista.Add(usuario);

                    }
                    data.Items = lista;
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
            return data;
        }

        public async Task<string> SavePlato(Plato obj)
        {
            var data = new Plato();
            string mensaje = "";
            var registro = 0;
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_guardarPlato", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_idRestaurante", obj.idRestaurante);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("p_descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("p_precio", obj.precio);
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
