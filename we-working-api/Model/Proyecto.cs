using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace we_working_api.Model
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public int LiderId { get; set; }
        public int EmpresaId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<ComentarioProyecto> ComentarioProyectos { get; set; }
        public virtual ICollection<EmpleadoProyecto> EmpleadoProyectos { get; set; }
        public virtual ICollection<TareaEmpleadoProyecto> TareaEmpleadoProyectos { get; set; }
    }
}
