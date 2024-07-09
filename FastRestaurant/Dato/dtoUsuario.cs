using FastRestaurant.Entidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace FastRestaurant.Dato
{
    public class dtoUsuario
    {
        string cadenaBD = "";
        public dtoUsuario(string cadena)
        {
            cadenaBD = cadena;
        }

        public async Task<DataCollection<Usuario>> GetListUsuario(paramGetListUsuario obj)
        {
            var data = new DataCollection<Usuario>();
            var lista = new List<Usuario>();
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_listarUsuario", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_usuario", obj.usuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        try { usuario.id = dr.GetInt32("id"); } catch (Exception ex) { usuario.id = null; }
                        try { usuario.usuario = dr.GetString("usuario"); } catch (Exception ex) { usuario.usuario = null; }
                        try { usuario.correo = dr.GetString("correo"); } catch (Exception ex) { usuario.correo = null; }
                        try { usuario.pwd = dr.GetString("pwd"); } catch (Exception ex) { usuario.pwd = null; }
                        try { usuario.nombre = dr.GetString("nombre"); } catch (Exception ex) { usuario.nombre = null; }
                        try { usuario.apellido = dr.GetString("apellido"); } catch (Exception ex) { usuario.apellido = null; }
                        try { usuario.swt = dr.GetInt32("swt"); } catch (Exception ex) { usuario.swt = null; }

                        lista.Add(usuario);

                    }
                    data.Items = lista;
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
            return data;
        }

        public async Task<string> SaveUsuario(Usuario obj)
        {
            var data = new Usuario();
            string mensaje = "";
            var registro = 0;
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_guardarUsuario", connection);
                    cmd.Parameters.AddWithValue("p_id", obj.id);
                    cmd.Parameters.AddWithValue("p_usuario", obj.usuario);
                    cmd.Parameters.AddWithValue("p_correo", obj.correo);
                    cmd.Parameters.AddWithValue("p_pwd", obj.pwd);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("p_apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("p_swt", obj.swt);
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

        public List<LoginEntidad> getTokenLogin(string correo, string contra)
        {
            List<LoginEntidad> lista = new List<LoginEntidad>();
            MySqlDataReader dr = null;
            try
            {
                using (var connection = new MySqlConnection(cadenaBD))
                {
                    connection.Open();

                    var result = connection.CreateCommand();
                    MySqlCommand cmd = new MySqlCommand("usp_login", connection);
                    cmd.Parameters.AddWithValue("p_correo", correo);
                    cmd.Parameters.AddWithValue("p_pwd", contra);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        lista.Add(new LoginEntidad() { usuario = Convert.ToString(dr["usuario"]), id = Convert.ToInt32(dr["id"]), nombre = Convert.ToString(dr["nombre"]), apellido = Convert.ToString(dr["apellido"]), correo = Convert.ToString(dr["correo"]) })   ;
                        
                    }
                }
                return lista;
            }
            catch (Exception ex) { return lista; }
        }

    }
}
