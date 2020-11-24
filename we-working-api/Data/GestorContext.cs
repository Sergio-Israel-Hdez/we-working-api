using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using we_working_api.Model;

namespace we_working_api.Data
{
    public class GestorContext:DbContext
    {
        public GestorContext(DbContextOptions<GestorContext> options):base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<TareaEmpleadoProyecto> TareaEmpleadoProyectos { get; set; }
        public DbSet<EmpleadoProyecto> EmpleadoProyectos { get; set; }
        public DbSet<ComentarioTarea> ComentarioTareas { get; set; }
        public DbSet<ComentarioProyecto> ComentarioProyectos { get; set; }
    }
}
