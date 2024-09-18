using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.OperacionesAlumno
{
    public class AlumnoDAO
    {
        // creamos el objeto ProyectoUdemyEntityContext para manupular las tablas
        private ProyectoUdemyEntityContext context = new ProyectoUdemyEntityContext();

        public async Task<List<Alumno>> SeleccionarTodo()
        {
            var alumno = await context.Alumnos.ToListAsync();
            return alumno;
        }
        public async Task<Alumno> Seleccionado(int id)
        {
            var alumnoSeleccionado = await context.Alumnos.Where(a => a.Id == id).FirstOrDefaultAsync();
            return alumnoSeleccionado;
        }
        public async Task<Alumno> SeleccionadoPorDni(string dni)
        {
            var alumnoSeleccionado = await context.Alumnos.Where(a => a.Dni.Equals(dni)).FirstOrDefaultAsync();
            return alumnoSeleccionado;
        }
        public bool Insertar(string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                Alumno nuevoAlumno = new Alumno();
                nuevoAlumno.Dni = dni;
                nuevoAlumno.Nombre = nombre;
                nuevoAlumno.Direccion = direccion;
                nuevoAlumno.Edad = edad;
                nuevoAlumno.Email = email;
                // lo agregamos en la bd
                context.Alumnos.Add(nuevoAlumno);

                context.SaveChanges(); // refrescamos
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //actualizar
        public async Task<bool> Actualizar(int id,string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                var alumno = await Seleccionado(id);
                if(alumno != null)
                {
                    alumno.Dni = dni;
                    alumno.Nombre = nombre;
                    alumno.Direccion = direccion;
                    alumno.Edad = edad;
                    alumno.Email = email;
                    // refrescamos
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var alumno = await Seleccionado(id);
                if(alumno != null)
                {
                    context.Alumnos.Remove(alumno);
                    context.SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        //Consuñlta multitabla
        public async Task<List<AlumnoAsignatura>> SeleccionarAlumnoAsignatura()
        {
            var query = from a in context.Alumnos
                        join m in context.Matriculas on a.Id equals m.Id
                        join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                        select new AlumnoAsignatura
                        {
                            NombreAlumno = a.Nombre,
                            NombreAsignatura = asig.Nombre 
                        };
            return await query.ToListAsync();
        }
        // listado de alumnos de los profesores
        public async Task<List<AlumnosDeProfesor>> SeleccionarAlumnosProfesores(string  usuario)
        {
            try
            {
                var query = from a in context.Alumnos
                            join m in context.Matriculas on a.Id equals m.AlumnoId
                            join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                            where asig.Profesor == usuario
                            select new AlumnosDeProfesor
                            {
                                Id = a.Id,
                                Dni = a.Dni,
                                Nombre = a.Nombre,
                                Direccion = a.Direccion,
                                Edad = a.Edad,
                                Email = a.Email,
                                Asignatura = asig.Nombre,
                                MatriculaId = m.Id,
                            };
                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //insertar y matricular
        public async Task<bool> insertarYmatricular(string dni, string nombre, string direccion, int edad, string email, int id_asig)
        {
            try
            {
                // buscamos el alumno seleccionado por dni
                var existente = await SeleccionadoPorDni(dni);
                if (existente == null)// no existe
                {
                    // insertamos
                    Insertar(dni, nombre, direccion, edad, email);
                    //buscamos el insertado y guardamos
                    var insertado = SeleccionadoPorDni(dni);
                    // creamos una nueva instancia de la matricula para matricularlo
                    Matricula m = new Matricula();
                    m.AlumnoId = insertado.Id;
                    m.AsignaturaId = id_asig;
                    // agregamos y refrescamos la bd
                    context.Matriculas.Add(m);
                    context.SaveChanges();
                }
                else // ya existe
                {
                    Matricula m = new Matricula();
                    m.AlumnoId = existente.Id;
                    m.AsignaturaId = id_asig;
                    // agregamos y refrescamos la bd
                    context.Matriculas.Add(m);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        // eliminar alumno con todas sus instancias
        public async Task<bool> EliminarAlumno(int id)
        {
            try
            {
                // buscamos el alumno
                var AlumnoBorrado = await context.Alumnos.Where(a => a.Id == id).FirstOrDefaultAsync();

                if (AlumnoBorrado != null) // no esta vacio
                {
                    // buscamos la matricula
                    var matriculasAlumno = context.Matriculas.Where(m => m.AlumnoId == id);
                    // recorremos las calificaiones y las guardamos
                    foreach(Matricula m in matriculasAlumno) 
                    {
                        // aca se guardas las notas de la matricula del alumno
                        var calificaciciones = context.Calificacions.Where(c => c.MatriculaId == m.Id);
                        // removemos los registros u objetos de califiaciones
                        context.Calificacions.RemoveRange(calificaciciones);
                    }
                    // eliminamos la matricula
                    context.Matriculas.RemoveRange(matriculasAlumno);
                    // eliminamos el alumno
                    context.Alumnos.RemoveRange(AlumnoBorrado);
                    // refresh
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
