using FastRestaurant.Dato;
using FastRestaurant.Entidad;   
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace FastRestaurant.Controllers
{
    [ApiController]
    [Route("Restaurant/")]
    public class FastRestaurantController : ControllerBase
    {
        static string cadenaDB = "datasource=18.226.165.232 ;port=7010;username=movil;password=Moviles1;database=db_fastrestaurant;";
        dtoUsuario dtousuario = new dtoUsuario(cadenaDB);
        dtoRestaurante dtorestaurante = new dtoRestaurante(cadenaDB);
        dtoPlato dtoplato= new dtoPlato(cadenaDB);
        dtoComentario dtocomentario = new dtoComentario(cadenaDB);
        List<LoginEntidad> lista = new List<LoginEntidad>();

        [HttpPost("PostLogin")]
        public async Task<IActionResult> GetLogin(Usuario obj)
        {
            lista = dtousuario.getTokenLogin(obj.correo, obj.pwd);
            if(lista.Count >= 1)
            {
                return Ok(new { lista, message = "Personal Autorizado" });
            }
            else
            {
                return BadRequest("Error al loguear");
            }
        }
        [HttpPost("GetListUsuario")]
        public async Task<IActionResult> GetListUsuario(paramGetListUsuario obj)
        {
                var result = await dtousuario.GetListUsuario(obj);
                return Ok(result);
         
        }
        [HttpPost("SaveUsuario")]
        public async Task<IActionResult> SaveUsuario(Usuario obj)
        {
                var result = await dtousuario.SaveUsuario(obj);
                return Ok(result);
        }





        [HttpPost("GetListRestaurante")]
        public async Task<IActionResult> GetListRestaurante(paramGetListRestaurante obj)
        {
            var result = await dtorestaurante.GetListRestaurante(obj);
            return Ok(result);

        }
        [HttpPost("SaveRestaurante")]
        public async Task<IActionResult> SaveRestaurante(Restaurante obj)
        {
            var result = await dtorestaurante.SaveRestaurante(obj);
            return Ok(result);
        }





        [HttpPost("GetListPlato")]
        public async Task<IActionResult> GetListPlato(paramGetListPlato obj)
        {
            var result = await dtoplato.GetListPlato(obj);
            return Ok(result);

        }
        [HttpPost("SavePlato")]
        public async Task<IActionResult> SavePlato(Plato obj)
        {
            var result = await dtoplato.SavePlato(obj);
            return Ok(result);
        }





        [HttpPost("GetListComentario")]
        public async Task<IActionResult> GetListComentario(paramGetListComentario obj)
        {
            var result = await dtocomentario.GetListComentario(obj);
            return Ok(result);

        }
        [HttpPost("SaveComentario")]
        public async Task<IActionResult> SaveComentario(Comentario obj)
        {
            var result = await dtocomentario.SaveComentario(obj);
            return Ok(result);
        }
    }
}
