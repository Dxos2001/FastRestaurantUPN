using FastRestaurant.Entidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace FastRestaurant.Dato
{
    public class dtoComentario
    {
        string cadenaBD = "";
        public dtoComentario(string cadena)
        {
            cadenaBD = cadena;
        }

        public async Task<DataCollection<Comentario>> GetListComentario(paramGetListComentario obj)
        {
            var data = new DataCollection<Comentario>();
            var lista = new List<Comentario>();
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_listarComentario", connection);
                    cmd.Parameters.AddWithValue("p_fdesde", obj.fdesde);
                    cmd.Parameters.AddWithValue("p_fhasta", obj.fhasta);
                    cmd.Parameters.AddWithValue("p_idUsuario", obj.idUsuario);
                    cmd.Parameters.AddWithValue("p_idRestaurante", obj.idRestaurante);
                    cmd.Parameters.AddWithValue("p_idPlato", obj.idPlato);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Comentario usuario = new Comentario();
                        try { usuario.id = dr.GetInt32("id"); } catch (Exception ex) { usuario.id = null; }
                        try { usuario.fecha = dr.GetDateTime("fecha"); } catch (Exception ex) { usuario.fecha = null; }
                        try { usuario.idUsuario = dr.GetInt32("idUsuario"); } catch (Exception ex) { usuario.idUsuario = null; }
                        try { usuario.valoracion = dr.GetInt32("valoracion"); } catch (Exception ex) { usuario.valoracion = null; }
                        try { usuario.comentario = dr.GetString("comentario"); } catch (Exception ex) { usuario.comentario = null; }
                        try { usuario.idRestaurante = dr.GetInt32("idRestaurante"); } catch (Exception ex) { usuario.idRestaurante = null; }
                        try { usuario.idPlato = dr.GetInt32("idPlato"); } catch (Exception ex) { usuario.idPlato = 0; }
                        lista.Add(usuario);

                    }
                    data.Items = lista;
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
            return data;
        }

        public async Task<string> SaveComentario(Comentario obj)
        {
            var data = new Comentario();
            string mensaje = "";
            var registro = 0;
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_guardarComentario", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("p_idUsuario", obj.idUsuario);
                    cmd.Parameters.AddWithValue("p_valoracion", obj.valoracion);
                    cmd.Parameters.AddWithValue("p_comentario", obj.comentario);
                    cmd.Parameters.AddWithValue("p_idRestaurante", obj.idRestaurante);
                    cmd.Parameters.AddWithValue("p_idPlato", obj.idPlato);
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
