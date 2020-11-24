using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace we_working_api.Model
{
    public class EmpleadoProyecto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }
        public virtual Proyecto Proyecto { get; set; }
    }
}
