using API_AppPousada.Models;
using API_AppPousada.Suporte;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AppPousada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario : ControllerBase
    {
        private readonly PousadaTesteContext _dbContext;

        public Usuario(PousadaTesteContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("verifica-login")]
        public async Task<IActionResult> Verifica(Login login)
        {
            string senhacripto = Seguranca.Criptografar(login.senha);

            var usuario = await _dbContext.TblUsuarios.Where(x =>
                x.Username == login.usuario && x.Password == senhacripto).FirstOrDefaultAsync();

            if (usuario != null && usuario.IdUsuario > 0)
            {
                return Ok(usuario);
            }

            else
            {
                return Unauthorized();
            }
        }
    }
}
