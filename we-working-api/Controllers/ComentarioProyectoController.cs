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
    public class ComentarioProyectoController : ControllerBase
    {
        private readonly ILogger<ComentarioProyectoController> _logger;
        private IRepository<ComentarioProyecto> _comentarioProyecto = null;
        private GestorContext _context = null;

        public ComentarioProyectoController(ILogger<ComentarioProyectoController> logger,GestorContext context)
        {
            _logger = logger;
            _context = context;
            _comentarioProyecto = new BaseRepository<ComentarioProyecto>(_context);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "2,3")]
        public IActionResult GetComentarioUsuarioProyecto(int idusuario)
        {
            var result = _comentarioProyecto.Get(filter: x => x.UsuarioId == idusuario, orderBy: x => x.OrderBy(x => x.FechaComentario), includeProperties: "Proyecto,Usuario");
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles ="1,2,3")]
        public IActionResult GetComentariosProyecto(int idproyecto)
        {
            var result = _comentarioProyecto.Get(filter:x=>x.ProyectoId == idproyecto,orderBy:x=>x.OrderBy(x=>x.FechaComentario),includeProperties:"Proyecto,Usuario");
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "1")]
        ///
        /// EndPoint solo para administrador
        ///         
        public IActionResult GetAllComentarioProyectos()
        {
            var result = _comentarioProyecto.Get(filter:null, orderBy: x => x.OrderBy(x => x.FechaComentario), includeProperties: "Proyecto");
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "2,3")]
        public void SaveComentarioProyecto([FromBody] ComentarioProyecto comentario)
        {
            _comentarioProyecto.Insert(comentario);
            _comentarioProyecto.Save();
        }
        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "1,2,3")]
        public void DeleteComentario(int idcomentario)
        {
            _comentarioProyecto.Delete(idcomentario);
            _comentarioProyecto.Save();
        }
        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "1,2,3")]
        public void EditComentarioProyecto([FromBody] ComentarioProyecto comentario)
        {
            _comentarioProyecto.Update(comentario);
            _comentarioProyecto.Save();
        }
    }
}
