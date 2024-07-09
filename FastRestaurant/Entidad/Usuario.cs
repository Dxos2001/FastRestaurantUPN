namespace FastRestaurant.Entidad
{
    public class Usuario
    {
        public int? id { get; set; }
        public string? usuario { get; set; }
        public string? correo { get; set; }
        public string? pwd { get; set; }
        public string? nombre { get; set; } 
        public string? apellido { get; set;}
        public int? swt { get; set;} 
    }
    public class paramGetListUsuario()
    {   
        public int? id { get; set; }
        public string? usuario { get; set; }
    }
}
