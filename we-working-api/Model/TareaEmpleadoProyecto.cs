using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace we_working_api.Model
{
    public class TareaEmpleadoProyecto
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public int EstadoTarea { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Proyecto Proyecto { get; set; }
        public virtual ICollection<ComentarioTarea> ComentarioTareas { get; set; }
    }
}
