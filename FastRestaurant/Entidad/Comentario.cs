    namespace FastRestaurant.Entidad
{
    public class Comentario
    {
        public int? id { get; set; }
        public DateTime? fecha { get; set; }
        public int? idUsuario { get; set; }
        public int? valoracion { get; set; } 
        public string? comentario { get; set; }
        public int? idRestaurante { get; set; }
        public int? idPlato { get; set; }
    }
    public class paramGetListComentario
    { 
        public DateTime? fdesde { get; set; }
        public DateTime? fhasta { get; set; }
        public int idUsuario { get; set; }
        public int idRestaurante { get; set; }
        public int idPlato { get; set; }
    }
}
