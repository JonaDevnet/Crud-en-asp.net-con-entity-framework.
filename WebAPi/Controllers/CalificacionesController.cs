using AccesoDatos.Models;
using AccesoDatos.OperacionesAlumno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private CalificacionesDAO calificacionesDAO = new CalificacionesDAO();
        [HttpGet("getCalificaciones")]
        public List<Calificacion> getCAlificaciones(int matriculaID)
        {
            return calificacionesDAO.GetCalificaciones(matriculaID);
        }
        [HttpPost("postCalificacion")]
        public bool agregarCalificacion([FromBody] Calificacion calificacion)
        {
            return calificacionesDAO.AgregarCalificacion(calificacion);
        }
        [HttpDelete("deleteCalificacion")]
        public bool eliminarCalificacion(int id)
        {
            return calificacionesDAO.EliminarCalificacion(id);
        }
    }
}
