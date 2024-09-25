using AccesoDatos.Models;
using AccesoDatos.OperacionesAlumno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDAO alumnoDAO = new AlumnoDAO();
        [HttpGet("alumnoProfesor")]
        public async Task<List<AlumnosDeProfesor>> seleccionarAlumProfe(string usuario)
        {
            return await alumnoDAO.SeleccionarAlumnosProfesores(usuario);
        }
        [HttpGet("datosAlumno")]
        public async Task<Alumno> alumnoDatos(int id)
        {
            return await alumnoDAO.Seleccionado(id);
        }
        //actualizamos
        [HttpPut("actualizarAlumno")]
        public async Task<bool> ActualizarAlumno([FromBody] Alumno alumno)
        {
            return await alumnoDAO.Actualizar(alumno.Id, alumno.Dni, alumno.Nombre, alumno.Direccion, alumno.Edad, alumno.Email);
        }
        //insertar y matricular
        [HttpPost("insertarYmatricular")]
        public async Task<bool> insertarYmatricular([FromBody] Alumno a, int id_asig)
        {
            return await alumnoDAO.insertarYmatricular(a.Dni, a.Nombre, a.Direccion, a.Edad, a.Email, id_asig);
        }
        [HttpDelete("eliminarAlumno")]
        public async Task<bool> eliminarALumno(int id)
        {
            return await alumnoDAO.EliminarAlumno(id);
        }
    }
}
