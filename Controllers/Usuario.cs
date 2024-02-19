using API_AppGerenciamento.Models;
using API_AppGerenciamento.Suporte;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AppGerenciamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario : ControllerBase
    {
        private readonly GerenciamentoContext _dbContext;

        public Usuario(GerenciamentoContext dbContext)
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
