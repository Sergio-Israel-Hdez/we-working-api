using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using we_working_api.Data;
using we_working_api.Data.Interfases;
using we_working_api.Data.Repository;
using we_working_api.Model;

namespace we_working_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemploController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<LoginController> _logger;
        private IRepository<Usuario> _usuario = null;
        private GestorContext _context = null;
        public EjemploController(ILogger<LoginController> logger, GestorContext context,IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _config = configuration;
            _usuario = new BaseRepository<Usuario>(_context);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(Usuario login)
        {
            return Ok("hola contenedor automatizado te saluda");
        }

    }
}
