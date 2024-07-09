namespace FastRestaurant.Entidad
{
    public class Restaurante
    {
        public int? id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string? direccion { get; set; }
        public string? telefono { get; set; }
        public string? imagen { get; set; }
    }
    public class paramGetListRestaurante
    {
        public int? id { get; set; }
        public string? nombre { get; set; }
    }
}
