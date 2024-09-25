using AccesoDatos.Context;
using AccesoDatos.Models;
using AccesoDatos.OperacionesAlumno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        public ProfesorDAO profeDAO = new ProfesorDAO();
        [HttpPost("autenticacion")]
        public string Login([FromBody] Profesor p)
        {
            try
            {
                var profesor = profeDAO.login(p.Usuario,p.Pass);

                if (profesor != null)
                {
                    return profesor.Usuario;
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
