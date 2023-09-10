using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using we_working_api.Data;
using we_working_api.Data.Interfases;
using we_working_api.Data.Repository;
using we_working_api.Model;

namespace we_working_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly ILogger<ProyectoController> _logger;
        private IRepository<Proyecto> _proyecto = null;
        private IRepository<EmpleadoProyecto> _empleadoProyecto = null;
        private GestorContext _context = null;
        public ProyectoController(ILogger<ProyectoController> logger, GestorContext context)
        {
            _logger = logger;
            _context = context;
            _proyecto = new BaseRepository<Proyecto>(_context);
            _empleadoProyecto = new BaseRepository<EmpleadoProyecto>(_context);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "1")]
        public IActionResult GetAllProyectos()
        {
            var result = _proyecto.Get(filter: null, orderBy: null, includeProperties: "ComentarioProyectos,EmpleadoProyectos,TareaEmpleadoProyectos");
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "2,3")]
        public IActionResult GetAllProyectosUsuario(int idusuario)
        {
            var resultEmpleado = _empleadoProyecto.Get(filter: x => x.UsuarioId == idusuario, orderBy: null, includeProperties: "Proyecto").FirstOrDefault();
            if (resultEmpleado==null)
            {
                return NotFound();
            }
            return Ok(resultEmpleado);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles ="1,2,3")]
        public IActionResult GetDetailProyecto(int idproyecto)
        {
            var result = _proyecto.Get(filter: x=>x.Id == idproyecto, orderBy: null, includeProperties: "ComentarioProyectos,EmpleadoProyectos,TareaEmpleadoProyectos").FirstOrDefault();
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetHola(int idproyecto)
        {
            string hola = "hola desde un contenedor,que fue automatizado";
            return Ok(hola);
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "2")]
        public void SaveProyecto([FromBody] Proyecto proyecto)
        {
            _proyecto.Insert(proyecto);
            _proyecto.Save();

        }
        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "1,2")]
        public void DeleteProyecto(int idproyecto)
        {
            _proyecto.Delete(idproyecto);
            _proyecto.Save();
        }
        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "1,2")]
        public void UpdateProyecto([FromBody] Proyecto proyecto)
        {
            _proyecto.Update(proyecto);
            _proyecto.Save();
        }
    }
}
