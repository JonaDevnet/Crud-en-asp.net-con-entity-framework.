using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.OperacionesAlumno
{
    public class CalificacionesDAO
    {
        private ProyectoUdemyEntityContext context = new ProyectoUdemyEntityContext();
        public List<Calificacion> GetCalificaciones(int matriculaId)
        {
            var calificaciones = context.Calificacions.Where(c => c.MatriculaId == matriculaId).ToList();
            return calificaciones;
        }
        //agregar calificacion
        public bool AgregarCalificacion(Calificacion calificacion)
        {
            try
            {
                context.Calificacions.Add(calificacion);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        // eliminar calificacion
        public bool EliminarCalificacion(int id)
        {
            try
            {
                // buscamos la calificacion
                var calificacion = context.Calificacions.Where(c => c.Id == id).FirstOrDefault();

                if(calificacion != null ) // si existe
                {
                    context.Calificacions.Remove(calificacion);
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
