using API_AppPousada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AppPousada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppVersion : ControllerBase
    {
        private readonly PousadaTesteContext _dbContext;

        public AppVersion(PousadaTesteContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("verifica-versao")]
        public async Task<IActionResult> VerificaVersao()
        {
            var ultimaLinha = await _dbContext.TblAppPousadaPeNaTerraVersaos
                                    .OrderByDescending(x => x.Id)
                                    .FirstOrDefaultAsync();

            if (ultimaLinha == null)
            {
                // Tratar o caso em que a tabela está vazia
                return NotFound();
            }

            return Ok(ultimaLinha);
        }

        [HttpPost]
        [Route("inserir-versao")]
        public async Task<IActionResult> InserirVersao(TblAppPousadaPeNaTerraVersao versionInfo)
        {
            try
            {
                var novaVersao = new TblAppPousadaPeNaTerraVersao
                {
                    Versao = versionInfo.Versao,
                    Data = versionInfo.Data
                };

                _dbContext.TblAppPousadaPeNaTerraVersaos.Add(novaVersao);
                await _dbContext.SaveChangesAsync();

                return Ok(novaVersao);
            }
            catch (Exception ex)
            {
                // Obtém a exceção interna, se houver
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest($"Erro ao salvar as alterações: {innerExceptionMessage}");
            }
        }
    }
}
