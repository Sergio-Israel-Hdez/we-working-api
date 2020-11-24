using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace we_working_api.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Rol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<ComentarioProyecto> ComentarioProyectos { get; set; }
        public virtual ICollection<Proyecto> Proyectos { get; set; }
        public virtual ICollection<EmpleadoProyecto> EmpleadoProyectos { get; set; }
        public virtual ICollection<TareaEmpleadoProyecto> TareaEmpleadoProyectos { get; set; }
        public virtual ICollection<ComentarioTarea> ComentarioTareas { get; set; }
    }
}
