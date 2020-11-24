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
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<LoginController> _logger;
        private IRepository<Usuario> _usuario = null;
        private GestorContext _context = null;
        public LoginController(ILogger<LoginController> logger, GestorContext context,IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _config = configuration;
            _usuario = new BaseRepository<Usuario>(_context);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Usuario login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);
            if (user!=null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private object GenerateJSONWebToken(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            IdentityModelEventSource.ShowPII = true;
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Nombre+" "+user.Apellido),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Rol.ToString()),
                new Claim(ClaimTypes.PrimarySid,user.Id.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Usuario AuthenticateUser(Usuario login)
        {
            var result = _usuario.Get(x => x.Email == login.Email && x.Password == login.Password, orderBy: null, includeProperties: "ComentarioProyectos,Proyectos,EmpleadoProyectos,TareaEmpleadoProyectos,ComentarioTareas").FirstOrDefault();
            return result;
        }
    }
}
