using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace we_working_api.Model
{
    public class ComentarioTarea
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TareaEmpleadoId { get; set; }
        public string Comentario { get; set; }
        public virtual TareaEmpleadoProyecto TareaEmpleadoProyecto { get; set; }
    }
}
