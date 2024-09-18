using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.OperacionesAlumno
{
    public class ProfesorDAO
    {
        public ProyectoUdemyEntityContext context = new ProyectoUdemyEntityContext();

        public Profesor login(string user, string pass)
        {
            try
            {
                var loginProfe = context.Profesors.Where(p => p.Usuario == user && p.Pass == pass).FirstOrDefault();
                return loginProfe;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
