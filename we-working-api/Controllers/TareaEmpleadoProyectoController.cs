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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TareaEmpleadoProyectoController : ControllerBase
    {
        private readonly ILogger<TareaEmpleadoProyectoController> _logger;
        private IRepository<TareaEmpleadoProyecto> _tarea = null;
        private GestorContext _context = null;
        public TareaEmpleadoProyectoController(ILogger<TareaEmpleadoProyectoController> logger, GestorContext context)
        {
            _logger = logger;
            _context = context;
            _tarea = new BaseRepository<TareaEmpleadoProyecto>(_context);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "1,2,3")]
        public IActionResult GetAllTareasProyecto(int idproyecto)
        {
            var result = _tarea.Get(filter: x => x.ProyectoId == idproyecto, orderBy: x => x.OrderBy(o => o.Id), includeProperties: "Usuario,Proyecto,ComentarioTareas");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "3")]
        public IActionResult GetAllTareasProyectoEmpleado(int idusuario)
        {
            var result = _tarea.Get(filter: x => x.UsuarioId == idusuario, orderBy: x => x.OrderBy(o => o.Id), includeProperties: "Usuario,Proyecto,ComentarioTareas");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "1,2,3")]
        public IActionResult GetDetailsTareaProyectoEmpleado(int idtarea)
        {
            var result = _tarea.GetById(idtarea);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "2")]
        public void AsignarTareaEmpleadoProyecto([FromBody] TareaEmpleadoProyecto tarea)
        {
            _tarea.Insert(tarea);
            _tarea.Save();
        }
        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "2")]
        public void EliminarTareaEmpleadoProyecto(int idtarea)
        {
            _tarea.Delete(idtarea);
            _tarea.Save();
        }
        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "2,3")]
        public void UpdateTareaEmpleadoProyecto([FromBody] TareaEmpleadoProyecto tarea)
        {
            _tarea.Update(tarea);
            _tarea.Save();
        }

    }
}
